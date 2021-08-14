using Newtonsoft.Json;
using PropertyChanged;
using ReceptionApp.Commands;
using ReceptionApp.Model;
using ReceptionApp.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class SubscVM : BaseVM, IDataErrorInfo
    {
        public ObservableCollection<string> PatientsName { get; set; }
        public ObservableCollection<string> DoctorsName { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public string SelectedDoctor { get; set; }
        public Prossedur Prossedur { get; set; }
        public SubscValidation Validation { get; set; }        
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public ICommand SubscribeBtn { get; set; }
               

        public SubscVM()
        {
            PatientsName = new ObservableCollection<string>();
            DoctorsName = new ObservableCollection<string>();
            Doctors = new ObservableCollection<Doctor>();

            var patients = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json");
            var jsPatients = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(patients);

            if (jsPatients != null)
            {

                foreach (var patient in jsPatients)
                {
                    PatientsName.Add(patient.Name);
                }
            }

            var doctors = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json");
            var jsDoctors = JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(doctors);
            Doctors = jsDoctors;
            if (jsDoctors != null)
            {

                foreach (var doctor in jsDoctors)
                {
                    DoctorsName.Add(doctor.Name);
                }
            }

            Date = DateTime.Now;
            Validation = new SubscValidation();               

            SubscribeBtn = new RelayCommand(x =>
            {
                try
                {
                    var selectedDoctor = Doctors.FirstOrDefault(x => x.Name == SelectedDoctor);

                    foreach (var schedule in selectedDoctor.Schedule.ScheduleElements)
                    {
                        if (schedule.OffDay == false && (schedule.BeginTime <= Time && schedule.EndTime >= Time) ||
                        (schedule.BreakBeginTime <= Time && schedule.BreakEndTime >= Time))
                        {
                            MessageBox.Show("This doctor is busy ! Please Select other time. ");
                            break;
                        }
                        else
                        {
                            MessageBox.Show("You are subscribed !");
                            break;
                        }
                    }
                }
                catch (Exception){ }   
               
            },()=>!string.IsNullOrWhiteSpace(Time.ToString()));
        }




        public string Error 
        {
            get
            {
                if (Validation != null)
                {
                    var results = Validation.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(' ', results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }

        public string this[string columnName] 
        {
            get
            {
                var firstOrDefault = Validation.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return Validation != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }
}
