using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using ReceptionApp.Commands;
using ReceptionApp.Model;
using ReceptionApp.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GenerUserPasswVM:BaseVM,IDataErrorInfo
    {        
        public ObservableCollection<string> PatientsName { get; set; }        
        public ObservableCollection<string> DoctorsName { get; set; }
        public GenerValidation GetValidationRules { get; set; }
        public string SelectedPatient { get; set; }
        public int SelectedIndex { get; set; }
        public int SelectedDoctorIndex { get; set; }
        public string PatientUsername { get; set; }
        public string PatientPassword { get; set; }
        public string DoctorUsername { get; set; }
        public string DoctorPassword { get; set; }
        public ICommand GenerPatBtn { get; set; }
        public ICommand GenerDocBtn { get; set; }
        public ICommand PSaveBtn { get; set; }
        public ICommand DSaveBtn { get; set; }

        public GenerUserPasswVM()
        {            
            PatientsName = new ObservableCollection<string>();            
            DoctorsName = new ObservableCollection<string>();

            GetValidationRules = new GenerValidation();

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
            var jsDoctors= JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(doctors);
            if (jsDoctors != null)
            {

            foreach (var doctor in jsDoctors)
            {
                DoctorsName.Add(doctor.Name);
            }
            }

            GenerPatBtn = new RelayCommand(x => 
            {
                PatientUsername = RandomStringForUsername(6);
                PatientPassword = RandomStringForPassword(6);
            });

            GenerDocBtn = new RelayCommand(x =>
            {
                  DoctorUsername = RandomStringForUsername(6);
                  DoctorPassword = RandomStringForPassword(6);
            });

            PSaveBtn = new RelayCommand(x =>
            {                              
                var patients = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json");
                dynamic jsPatients = JsonConvert.DeserializeObject(patients);
                jsPatients[SelectedIndex]["Username"] = PatientUsername;
                jsPatients[SelectedIndex]["Password"] = PatientPassword;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsPatients, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Patient.json", output);

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.ConnectionStrings["Email"].ConnectionString,
                    ConfigurationManager.ConnectionStrings["Password"].ConnectionString);//-----------------------------------
                                                                                         // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress(ConfigurationManager.ConnectionStrings["Email"].ConnectionString),
                        Subject = "Response from the reception",
                        Body = $"Your New Username: {PatientUsername}\n\nYour New Password: {PatientPassword}"
                    };

                    string email = jsPatients[SelectedIndex]["Mail"];
                    mail.To.Add(new MailAddress(email));//jsPatients[SelectedIndex]["Mail"]
                                                        // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = credentials
                    };
                    // Send it...         
                    client.Send(mail);//-------------------------------------------------------------------------------------------

                    PatientUsername = string.Empty;  PatientPassword = string.Empty;
                    MessageBox.Show("Username and password have been saved !");
                }
                catch (Exception ex){ MessageBox.Show(ex.Message); }   
                
                //-----------------------------
            }, () => !string.IsNullOrWhiteSpace(PatientUsername) && !string.IsNullOrWhiteSpace(PatientPassword) && string.IsNullOrWhiteSpace(Error));

            DSaveBtn = new RelayCommand(x =>
            {
                var doctors = File.ReadAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json");
                dynamic jsDoctors = JsonConvert.DeserializeObject(doctors);
                jsDoctors[SelectedDoctorIndex]["Username"] = DoctorUsername;
                jsDoctors[SelectedDoctorIndex]["Password"] = DoctorPassword;

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsDoctors, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Directory.GetParent("aasas").ToString()).ToString()).ToString()).ToString()).ToString()).ToString() + "\\MainDB\\" + "Doctor.json", output);

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.ConnectionStrings["Email"].ConnectionString,
                   ConfigurationManager.ConnectionStrings["Password"].ConnectionString);//-----------------------------------
                                                                                        // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress(ConfigurationManager.ConnectionStrings["Email"].ConnectionString),
                        Subject = "Response from the reception",
                        Body = $"Your Username: {DoctorUsername}\n\nYour Password: {DoctorPassword}"
                    };

                    string email = jsDoctors[SelectedDoctorIndex]["Mail"];
                    mail.To.Add(new MailAddress(email));
                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = credentials
                    };
                    // Send it...         
                    client.Send(mail);//-------------------------------------------------------------------------------------------

                    DoctorUsername = string.Empty;  DoctorPassword = string.Empty;
                    MessageBox.Show("Username and password have been saved !");
                }
                catch (Exception ex){ MessageBox.Show(ex.Message); }                

                
            }, () => !string.IsNullOrWhiteSpace(DoctorUsername) && !string.IsNullOrWhiteSpace(DoctorPassword) && string.IsNullOrWhiteSpace(Error));
        }

        public static string RandomStringForUsername(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomStringForPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789_";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string Error
        {
            get
            {
                if (GetValidationRules != null)
                {
                    var results = GetValidationRules.Validate(this);
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
                var firstOrDefault = GetValidationRules.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return GetValidationRules != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }
    }
}
