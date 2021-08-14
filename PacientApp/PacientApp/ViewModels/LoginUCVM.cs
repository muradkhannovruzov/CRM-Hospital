using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using PacientApp.Messaging;
using PacientApp.Validation;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginUCVM : ViewModelBase, IDataErrorInfo
    {
        readonly LoginUCVMValidation validator;

        readonly IRepository<Patient> repository;

        readonly IMessenger messenger;
        public LoginUCVM(IRepository<Patient> repository, IMessenger messenger)
        {
            this.messenger = messenger;

            this.repository = repository;

            validator = new LoginUCVMValidation();

            LoginCommand = new RelayCommand<object>(LoginCommandExecute);

            RegisterCommand = new RelayCommand<object>(RegisterCommandExecute);
        }

        public string Username { get; set; } 
        public string Password { get; set; } 
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        private void LoginCommandExecute(object obj)
        {
            if (Error == string.Empty)
            {
                var patient = repository.GetAll().FirstOrDefault(p => p.Username == Username && p.Password == Password);

                if (patient != null)
                {
                    GetPatientData(patient);

                    messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<HomeUCVM>() });
                }
            }
        }
        private void RegisterCommandExecute(object obj)
        {
            messenger.Send(new VMChange() { ViewModel = App.Container.GetInstance<RegisterUCVM>() });
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
