using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using PacientApp.Messaging;
using PacientApp.Validation;
using PacientApp.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    public class RegisterUCVM : ViewModelBase, IDataErrorInfo
    {
        readonly IRepository<Patient> repository;

        readonly IMessenger messenger;

        readonly RegisterUCVMValidator validator;

        public RegisterUCVM(IRepository<Patient> repository, IMessenger messenger)
        {
            this.repository = repository;

            this.messenger = messenger;

            validator = new RegisterUCVMValidator();

            RegisterCommand = new RelayCommand<object>(RegisterCommandExecute);

            LoginCommand = new RelayCommand<object>(LoginCommandExecute);
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime  BirthDay { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }

        public ICommand RegisterCommand { get; private set; }

        public ICommand LoginCommand { get; private set; }

        private void RegisterCommandExecute(object obj)
        {
            if (Error == string.Empty)
            {
                if (repository.GetAll().FirstOrDefault(p => p.Username == Username) == null)
                {
                    var verify = new VerifyWindow(Mail);

                    verify.ShowDialog();

                    if (verify.DialogResult == true)
                    {

                        var patient = new Patient()
                        {
                            Id = Guid.NewGuid().ToString(),
                            BirthDate = BirthDay,
                            Name = Name,
                            Mail = Mail,
                            Number = Number,
                            Surname = Surname,
                            Password = Password,
                            Username = Username,
                        };

                        repository.Add(patient);

                        GetPatientData(patient);

                        messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<HomeUCVM>() });
                    }
                }
            }
        }

        private void LoginCommandExecute(object obj)
        {
            messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<LoginUCVM>() });
        }

        public string Error
        {
            get
            {
                if (validator != null)
                {
                    var result = validator.Validate(this);
                    if (result != null && result.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, result.Errors.Select(x => x.ErrorMessage).ToArray());
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
                var result = validator.Validate(this);

                if (!result.IsValid)
                {
                    var error = result.Errors.FirstOrDefault(x => x.PropertyName.Contains(columnName));
                    if (error != null)
                        return error.ErrorMessage;
                }
                return string.Empty;
            }
        }

        private void GetPatientData(Patient patient)
        {
            App.Container.GetInstance<Patient>().Username = patient.Username;
            App.Container.GetInstance<Patient>().Password = patient.Password;
            App.Container.GetInstance<Patient>().Name = patient.Name;
            App.Container.GetInstance<Patient>().Surname = patient.Surname;
            App.Container.GetInstance<Patient>().BirthDate = patient.BirthDate;
            App.Container.GetInstance<Patient>().Mail = patient.Mail;
            App.Container.GetInstance<Patient>().Number = patient.Number;

            App.Container.GetInstance<Patient>().CurrentAnalysesId = patient.CurrentAnalysesId;
            App.Container.GetInstance<Patient>().ProssedursId = patient.ProssedursId;
            App.Container.GetInstance<Patient>().HistoryBook = patient.HistoryBook;
        }
    }
}
