using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using PacientApp.Messaging;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
        [AddINotifyPropertyChangedInterface]
    public class ProcedureUCVM : ViewModelBase
    {
        public ICommand AddPossedure { get; set; }
        public ICommand SelectedCategoriaCommand { get; set; }
        public ICommand SelectedDoctorCommand { get; set; }

        private IMessenger Messenger;
        private IRepository<Patient> PatientRepo;
        private IRepository<Prossedur> ProssedurRepo;
        private IRepository<Doctor> DoctorRepo;
        private IFileService FileService;

        public List<string> ProssedurCats { get; set; }
        public string SelectedProssedurCat { get; set; }
        public string CurrentPatientDescription { get; set; }
        public string Error { get; set; }
        public string SelectedDoctorCat { get; set; }
        public DateTime SelectedDate { get; set; }
        public DateTime SelectedTime { get; set; }
        public Patient CurrentUser { get; set; }
        public Prossedur NewProssedure { get; set; }
        public Doctor SelectedDoctor { get; set; }
        public ObservableCollection<Doctor> InsterestedDoctors { get; set; } = new ObservableCollection<Doctor>();
        public List<Doctor> Doctors { get; set; }
        public List<DoctorCategories> DoctorsCats { get; set; }
        public ProcedureUCVM(IRepository<Patient> repository, IFileService fileService, IMessenger messenger, Patient patient)
        {
            Messenger = messenger;
            CurrentUser = patient;
            FileService = fileService;
            ProssedurRepo = new Repository<Prossedur>(FileService);
            DoctorRepo = new Repository<Doctor>(FileService);
            PatientRepo = new Repository<Patient>(FileService);
            DoctorsCats = new List<DoctorCategories>(Enum.GetValues(typeof(DoctorCategories)).Cast<DoctorCategories>().ToList());


            //getting info from db 
            Doctors = new List<Doctor>(DoctorRepo.GetAll());


            //
            AddPossedure = new RelayCommand(() => {
                bool everythingIsOk = true;
                var dateAndTime = SelectedDate.Add(SelectedTime.TimeOfDay);
                var dayOfWeek = dateAndTime.DayOfWeek;
                if (SelectedDoctor.Schedule.ScheduleElements.Count > 7)
                {
                    int sum = SelectedDoctor.Schedule.ScheduleElements.Count -8;
                    for (int i = sum; i >= 0; i--)
                    {
                        SelectedDoctor.Schedule.ScheduleElements.RemoveAt(i);
                    }
                }
                var scheduler = SelectedDoctor.Schedule.ScheduleElements.FirstOrDefault(x => x.DayOfWeek == dayOfWeek.ToString());
                if (SelectedDoctor.Pacients.Count > 0)
                {
                    var selectedDoctorProssedurId = SelectedDoctor.ProssedurId.ToList();
                    List<Prossedur> selectedDoctorProssedures = new List<Prossedur>();
                    List<Prossedur> AllProssedures = new List<Prossedur>(ProssedurRepo.GetAll());
                    for (int i = 0; i < selectedDoctorProssedurId.Count; i++)
                    {
                        foreach (var item in AllProssedures)
                        {
                            if (item.Id == selectedDoctorProssedurId[i])
                            {
                                selectedDoctorProssedures.Add(item);
                            }

                        }
                    }
                    if (selectedDoctorProssedures.Count > 0)
                    {
                        foreach (var item in selectedDoctorProssedures)
                        {
                            if (dateAndTime >= item.DateBegin && dateAndTime <= item.DateEnd)
                            {
                                everythingIsOk = false;
                            }
                        }
                    }
                }
                if (scheduler.OffDay == false && everythingIsOk && (scheduler.BeginTime < dateAndTime.TimeOfDay && scheduler.EndTime > dateAndTime.TimeOfDay)
                && (scheduler.BreakBeginTime > dateAndTime.TimeOfDay || scheduler.BreakEndTime < dateAndTime.TimeOfDay))
                {
                    Error = string.Empty;
                    Enum.TryParse(SelectedProssedurCat, out ProssedurCategories selectedcategoria);
                    var room = SelectedDoctor.DoctorCanMake.FirstOrDefault(item => item.NameOfProsedur == SelectedProssedurCat).Room;
                    var devices = SelectedDoctor.DoctorCanMake.FirstOrDefault(x => x.NameOfProsedur == SelectedProssedurCat).UsingDevice.ToList();
                    var medicines = SelectedDoctor.DoctorCanMake.FirstOrDefault(x => x.NameOfProsedur == SelectedProssedurCat).UsingMedicine.ToList();
                    var price = SelectedDoctor.DoctorCanMake.FirstOrDefault(x => x.NameOfProsedur == SelectedProssedurCat).Price;
                    int prossedurTime = int.Parse(SelectedDoctor.DoctorCanMake.FirstOrDefault(item => item.NameOfProsedur == SelectedProssedurCat).TimeOfProsedur);
                    NewProssedure = new Prossedur()
                    {
                        UsingDevice = devices,
                        UsingMedicine = medicines,
                        DateBegin = dateAndTime,
                        DrName = SelectedDoctor.Name,
                        PatientId = CurrentUser.Id,
                        PatientName=CurrentUser.Name,
                        PatientSurname=CurrentUser.Surname,
                        Room = room,
                        Price= price,
                        DateEnd = dateAndTime.AddHours(prossedurTime),
                        Id = Guid.NewGuid().ToString(),
                        Categoria = selectedcategoria,
                        PatientDescription = CurrentPatientDescription

                    };

                    CurrentUser.ProssedursId.Add(NewProssedure.Id);
                    ProssedurRepo.Add(NewProssedure);
                    if (!SelectedDoctor.Pacients.Contains(CurrentUser.Id))
                    {
                        SelectedDoctor.Pacients.Add(CurrentUser.Id);
                    }
                    SelectedDoctor.ProssedurId.Add(NewProssedure.Id);
                    DoctorRepo.Update(x =>
                    {
                        return true;
                    }, SelectedDoctor);

                    PatientRepo.Update(x => {
                        return true;

                    }, CurrentUser);

                   
                    Messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<HomeUCVM>() });
                }
                else
                {
                    Error = "This Doctor is busy for this time , Please Select another doctor or another Time";
                }

            });
            SelectedCategoriaCommand = new RelayCommand(() =>
            {

                if (SelectedDoctorCat != null)
                {
                    InsterestedDoctors.Clear();
                    foreach (var item in Doctors)
                    {
                        if (item.Categoria.ToString() == SelectedDoctorCat)
                        {
                            InsterestedDoctors.Add(item);
                        }

                    }
                }
            });
            SelectedDoctorCommand = new RelayCommand(() => {
                if (SelectedDoctor != null)
                {
                    ProssedurCats = new List<string>();
                    foreach (var item in SelectedDoctor.DoctorCanMake)
                    {
                        ProssedurCats.Add(item.NameOfProsedur);
                    }
                }
            });
        }
    }
}
