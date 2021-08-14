using Newtonsoft.Json;
using PropertyChanged;using ReceptionApp.Commands;using ReceptionApp.MediatorClass;using ReceptionApp.Messages;using ReceptionApp.Model;
using ReceptionApp.Validations;using System;using System.Collections.ObjectModel;using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;using System.Windows;using System.Windows.Input;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class AddPAtientVM:BaseVM,IDataErrorInfo
    {
        public PatValidation PatValidation { get; set; }
        public Regex NumberRegex { get; set; }
        public Regex NameRegex { get; set; }
        public Regex SurnameRegex { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ICommand AddPBtn { get; set; }       
        
        public AddPAtientVM()
        {
            NumberRegex = new Regex("[^0-9.-]+");
            NameRegex = new Regex("[^a-zA-Z]+$");
            SurnameRegex = new Regex("[^a-zA-Z]+$");
            Birthdate = DateTime.Today;
            PatValidation = new PatValidation();            
            Patients = new ObservableCollection<Patient>();
            AddPBtn = new RelayCommand(x => 
            {
                var patient = new Patient() { Name = Name, Surname = Surname, Number = Number, BirthDate = Birthdate, Mail = Mail };

                var patients = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json");
                var JSPatients = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(patients);
                Patients = JSPatients;
                if (Patients != null)
                { 
                    Patients.Add(patient);                      
                }
                else
                {
                    Patients = new ObservableCollection<Patient>();
                    Patients.Add(patient);                      
                }
                
                Name = string.Empty; Surname = string.Empty; Number = string.Empty; Mail = string.Empty;                

                var jsPatients = JsonConvert.SerializeObject(Patients, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json", jsPatients);

                MessageBox.Show("Patient was added !");
               
            }, () => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && !string.IsNullOrWhiteSpace(Number) && !NumberRegex.IsMatch(Number)&&
               !NameRegex.IsMatch(Name) && !SurnameRegex.IsMatch(Surname) && !string.IsNullOrWhiteSpace(Mail) && string.IsNullOrEmpty(Error));
        }

        public string Error 
        {
            get
            {
                if (PatValidation != null)
                {
                    var results = PatValidation.Validate(this);
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
                var firstOrDefault = PatValidation.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return PatValidation != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }
}
