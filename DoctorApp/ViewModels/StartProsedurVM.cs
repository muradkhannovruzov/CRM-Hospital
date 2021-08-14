using DoctorApp.Messenging;
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.ViewModels
{
    class StartProsedurVM : ViewModelBase
    {
        private Messenger messanger;
        private Prossedur prossedur;
        private Patient patient;
        private ViewModelBase currentViewModel2;
        private RelayCommand infoCommand;
        private RelayCommand sendCommand;
        private RelayCommand diagnisisCommand;
        private RelayCommand resipeCommand;
        private RelayCommand writeProsCommand;
        private RelayCommand endCommand;
        private string nameSurname;

        public RelayCommand EndCommand => endCommand ?? (endCommand = new RelayCommand(() =>
        {
            CurrentViewModel2 = App.Container.GetInstance<VoteVM>();
        }));

        public RelayCommand WriteProsCommand => writeProsCommand ?? (writeProsCommand = new RelayCommand(() =>
            {
                CurrentViewModel2 = App.Container.GetInstance<ProssedurWritingVM>();
            }));

        public RelayCommand ResipeCommand => resipeCommand ?? (resipeCommand = new RelayCommand(() =>
             {
                 CurrentViewModel2 = App.Container.GetInstance<PRecipeVM>();
             }));

        public RelayCommand DiagnisisCommand => diagnisisCommand ?? (diagnisisCommand = new RelayCommand(() =>
         {
             CurrentViewModel2 = App.Container.GetInstance<PDiagnosisVM>();
         }));

        public RelayCommand SendCommand => sendCommand ?? (sendCommand = new RelayCommand(() =>
        {
            CurrentViewModel2 = App.Container.GetInstance<PAnalysisVM>();
        }));

        public RelayCommand InfoCommand => infoCommand ?? (infoCommand = new RelayCommand(() =>
         {
             CurrentViewModel2 = App.Container.GetInstance<PInfoVM>();
         }));

        public ViewModelBase CurrentViewModel2 { get => currentViewModel2; set => Set(ref currentViewModel2, value); }

        public Patient Patient
        {
            get => patient;
            set
            {
                Set(ref patient, value);
                NameSurname = Patient.Name + ' ' + Patient.Surname;
            }
        }
        public Prossedur Prossedur { get => prossedur; set => Set(ref prossedur, value); }
        public string NameSurname { get => nameSurname; set => Set(ref nameSurname, value); }
        public StartProsedurVM(Messenger messenger)
        {
            this.messanger = messenger;

            messenger?.Register<CurrentProssChange>(this, x =>
             {
                 Prossedur = x.CurrentProssedur;
             });
            messenger?.Register<CurrentPatientChange>(this, x =>
            {
                Patient = x.CrPatient;
            });
            CurrentViewModel2 = App.Container.GetInstance<PInfoVM>();
        }
    }
}
