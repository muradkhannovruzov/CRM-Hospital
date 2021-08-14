using Newtonsoft.Json;using PropertyChanged;using ReceptionApp.Commands;using ReceptionApp.MediatorClass;using ReceptionApp.Messages;using ReceptionApp.Model;using ReceptionApp.Validations;
using System;using System.Collections.ObjectModel;using System.ComponentModel;using System.IO;using System.Linq;using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class AddDoctorVM : BaseVM, IDataErrorInfo
    {        
        public ICommand AddBtn { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }        
        public DocValidation DocValidation { get; set; }
        public Regex NameRegex { get; set; }
        public Regex SurnameRegex { get; set; }
        public Regex NumberRegex { get; set; }
        public string Name { get; set; }        
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }          
        public DoctorCategories DoctorCategory { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        
        public AddDoctorVM()
        {
            NameRegex = new Regex("[^a-zA-Z]+$");
            SurnameRegex = new Regex("[^a-zA-Z]+$");
            NumberRegex = new Regex("[^0-9.-]+");
            Birthdate = DateTime.Today;
            DocValidation = new DocValidation();            
            Doctors = new ObservableCollection<Doctor>();
            AddBtn = new RelayCommand(x =>
            {
                var rooms = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Room.json");
                var jsRooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(rooms);
                var Room = jsRooms.FirstOrDefault(x => x.RoomType == RoomType);

                var doctor = new Doctor()
                {
                    Name = Name,
                    Surname = Surname,
                    BirthDate = Birthdate,
                    Categoria = DoctorCategory,
                    DefaultRoom = Room,
                    Mail = Email,
                    Number = Number
                };              
                
                var doctors = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json");
                var JSDoctors = JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(doctors);
                Doctors = JSDoctors;
                if (Doctors != null)
                {
                    Doctors.Add(doctor);
                }
                else
                {
                    Doctors = new ObservableCollection<Doctor>();
                    Doctors.Add(doctor);
                }

                foreach (var Doctor in Doctors)
                {
                    if (Doctor.Schedule.ScheduleElements.Count > 7)
                    {
                        int sum = Doctor.Schedule.ScheduleElements.Count - 7;
                        for (int i = sum; i > 0; i--)
                        {
                            Doctor.Schedule.ScheduleElements.RemoveAt(i);
                        }
                    }
                }
                
                Name = string.Empty; Surname = string.Empty; Email = string.Empty; Number = string.Empty;

                var jsDoctors = JsonConvert.SerializeObject(Doctors, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent("aasas").ToString())
                .ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json", jsDoctors);

                MessageBox.Show("Doctor was added !");
              
            }, () =>!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && !NameRegex.IsMatch(Name) && !SurnameRegex.IsMatch(Surname) && 
               !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Number) && !NumberRegex.IsMatch(Number) &&  string.IsNullOrEmpty(Error));
        }

        public string Error
        {
            get
            {
                if (DocValidation != null)
                {
                    var results = DocValidation.Validate(this);
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
                var firstOrDefault = DocValidation.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return DocValidation != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }
}
