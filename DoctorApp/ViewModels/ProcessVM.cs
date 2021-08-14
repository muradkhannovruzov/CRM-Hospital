using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Repository;
using DoctorApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class ProcessVM : ViewModelBase
    {
        private IRepository<Doctor> DocRep;
        private IRepository<Patient> PatRep;
        private IRepository<Prossedur> ProRep;
        private IRepository<Room> RoomRep;
        private IRepository<Device> DevRep;

        private RelayCommand nextCommand;
        private RelayCommand thisDayCommand;
        private RelayCommand allCommand;
        private RelayCommand reloadCommand;
        private RelayCommand startCommand;
        public RelayCommand DeleteCommand => deleteCommand ?? (deleteCommand = new RelayCommand(() =>
         {
             double mebleg = Convert.ToDouble(SelectedProssedur.Price);


             SelectedProssedur.Status = Status.Canceled;

             SelectedProssedur.Room.IsEmpty = true;

             if (SelectedProssedur.UsingDevice != null)
             {
                 foreach (var item in SelectedProssedur.UsingDevice)
                 {
                     item.IsBusy = true;
                     mebleg -= Convert.ToDouble(item.Profit);

                     DevRep.Update(x => { return x.Name == item.Name; }, item);
                 }
             }
             if (SelectedProssedur.UsingMedicine != null)
             {
                 foreach (var item in SelectedProssedur.UsingMedicine)
                 {
                     mebleg -= Convert.ToDouble(item.Price) * item.Count;
                 }
             }

             mebleg = mebleg * Doctor.MainProfitPercent / 100;
             Doctor.OldPacients.Add(new OldPatient() { Name = Patient.Name, Money = mebleg, DateTime = DateTime.Now });

             Patient.Raiting.AddVote(new Vote() { Mail = Doctor.Mail, StarPoint = 0, Username = Doctor.Username });
             Patient.HistoryBook.ProssedursId.Add(SelectedProssedur.Id);
             Patient.ProssedursId.Remove(SelectedProssedur.Id);

             Doctor.OldProssedurId.Add(SelectedProssedur.Id);
             Doctor.Pacients.Add(Patient.Id);
             Doctor.ProssedurId.Remove(SelectedProssedur.Id);

             RoomRep.Update(x => { return x.Number == SelectedProssedur.Room.Number; }, SelectedProssedur.Room);


             DocRep.Update(x => { return x.Id == Doctor.Id; }, Doctor);
             PatRep.Update(x => { return x.Id == Patient.Id; }, Patient);
             ProRep.Update(x => { return x.Id == SelectedProssedur.Id; }, SelectedProssedur);

             if (HowReload)
                 Prossedurs = prossedurServices.FindSpecificDayProssedurs(Doctor.ProssedurId, DateTime.Now) as ObservableCollection<Prossedur>;
             else Prossedurs = prossedurServices.FindProssedurs(Doctor.ProssedurId) as ObservableCollection<Prossedur>;


         }, () => Prossedurs != null && Prossedurs.Count != 0
          && SelectedProssedur != null
          && SelectedProssedur.DateEnd < DateTime.Now
          && (SelectedProssedur.Status == Status.Canceled 
         || SelectedProssedur.Status == Status.NotCame
         || SelectedProssedur.Status == Status.HasCame
         || SelectedProssedur.Status == Status.Waiting)));

        private Messenger messenger;
        private IProssedurServices prossedurServices;
        private IPatientServices patientServices;
        private ObservableCollection<Prossedur> prossedurs;
        public bool HowReload
        {
            get => howReload; 
            set
            {
                Set(ref howReload, value);
                ThisDayCommand.RaiseCanExecuteChanged();
                AllCommand.RaiseCanExecuteChanged();
            }
        }
        private Prossedur selectedProssedur;
        private Patient patient;
        private Visibility isStart = Visibility.Collapsed;
        private string startContent;
        private RelayCommand deleteCommand;
        private bool howReload = true;

        public Doctor Doctor { get; set; }
        public Prossedur SelectedProssedur
        {
            get => selectedProssedur; set
            {
                Set(ref selectedProssedur, value);
                Patient = patientServices.FindPatient(SelectedProssedur?.PatientId);
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Prossedur> Prossedurs { get => prossedurs; set => Set(ref prossedurs, value); }
        public Patient Patient { get => patient; set => Set(ref patient, value); }
        public Visibility IsStart { get => isStart; set => Set(ref isStart, value); }
        public string StartContent { get => startContent; set => Set(ref startContent, value); }

        public ProcessVM(Messenger messenger, IProssedurServices prossedurServices, IPatientServices patientServices, IRepository<Doctor> docRep, 
            IRepository<Patient> patRep, IRepository<Prossedur> proRep, 
            IRepository<Room> roomRep, IRepository<Device> devRep)
        {
            this.messenger = messenger;
            this.prossedurServices = prossedurServices;
            this.patientServices = patientServices;
            DocRep = docRep;
            PatRep = patRep;
            DevRep = devRep;
            ProRep = proRep;
            RoomRep = roomRep;
            messenger.Register<CurrentDrChange>(this, x =>
            {
                Doctor = x.CurrnetDr;
                Prossedurs = prossedurServices.FindSpecificDayProssedurs(Doctor.ProssedurId, DateTime.Now) as ObservableCollection<Prossedur>;
            }, true);
        }
        public RelayCommand StartCommand => startCommand ?? (startCommand = new RelayCommand(() =>
          {
              if (SelectedProssedur == null || SelectedProssedur.Id != Prossedurs[0].Id)
              {
                  StartContent = "Select the next procedure";
                  IsStart = Visibility.Visible;
              }
              else if (SelectedProssedur?.DateEnd <= DateTime.Now)
              {
                  StartContent = "Prossedur's time is up, please delete";
                  IsStart = Visibility.Visible;
              }
              else if (SelectedProssedur?.DateBegin > DateTime.Now)
              {
                  StartContent = "the selected procedure is not the start time";
                  IsStart = Visibility.Visible;
              }
              else if (SelectedProssedur?.Status != Status.Waiting && SelectedProssedur?.Status != Status.InProcess)
              {
                  StartContent = "the selected procedure is not waiting";
                  IsStart = Visibility.Visible;
              }
              else
              {
                  messenger.Send(new CurrentProssChange() { CurrentProssedur = SelectedProssedur });
                  messenger.Send(new CurrentPatientChange() { CrPatient = Patient });
                  SelectedProssedur.Status = Status.InProcess;
                  ProRep.Update(x => { return x.Id == SelectedProssedur.Id; }, SelectedProssedur);
                  IsStart = Visibility.Collapsed;

                  SelectedProssedur.Room.IsEmpty = false;
                  RoomRep.Update(x => { return x.Number == SelectedProssedur.Room.Number; }, SelectedProssedur.Room);
                  messenger.Send(new ViewChange() { ViewModel = App.Container.GetInstance<StartProsedurVM>() });
              }
          }));


        public RelayCommand ReloadCommand => reloadCommand ?? (reloadCommand = new RelayCommand(() =>
        {
            if (HowReload)
                Prossedurs = prossedurServices.FindSpecificDayProssedurs(Doctor.ProssedurId, DateTime.Now) as ObservableCollection<Prossedur>;
            else Prossedurs = prossedurServices.FindProssedurs(Doctor.ProssedurId) as ObservableCollection<Prossedur>;
        }));

        public RelayCommand AllCommand => allCommand ?? (allCommand = new RelayCommand(() =>
        {
            Prossedurs = prossedurServices.FindProssedurs(Doctor.ProssedurId) as ObservableCollection<Prossedur>;
            HowReload = false;
        }, ()=> HowReload));

        public RelayCommand ThisDayCommand => thisDayCommand ?? (thisDayCommand = new RelayCommand(() =>
           {
               Prossedurs = prossedurServices.FindSpecificDayProssedurs(Doctor.ProssedurId, DateTime.Now) as ObservableCollection<Prossedur>;
               HowReload = true;
           }, () => !HowReload));

        public RelayCommand NextCommand => nextCommand ?? (nextCommand = new RelayCommand(() =>
           {
               Prossedurs = prossedurServices.FindSpecificDayProssedurs(Doctor.ProssedurId, DateTime.Now) as ObservableCollection<Prossedur>;
               if (Prossedurs != null && Prossedurs.Count != 0)
               {
                   HowReload = true;
                   SelectedProssedur = Prossedurs[0];
               }
               HowReload = true;
           }));

    }
}
