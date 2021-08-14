using DoctorApp.Models;
using DoctorApp.Messenging;
using DoctorApp.Repository;
using DoctorApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class PAnalysisVM : ViewModelBase
    {
        private Messenger messenger;
        private IRepository<Analysis> anaRep;
        private IRepository<Patient> patRep;
        private string name;
        private RelayCommand sendCommand;

        public RelayCommand SendCommand => sendCommand ?? (sendCommand = new RelayCommand(() =>
           {
               var a = new Analysis() { Name = Name };
               Patient.CurrentAnalysesId.Add(a.Id);
               patRep.Update(x => { return x.Id == Patient.Id; }, Patient);
               anaRep.Add(a);
               MessageBox.Show("Analysis sended");
               Name = string.Empty;
           }, ()=>!string.IsNullOrWhiteSpace(Name)));

        public Patient Patient { get; set; }
        public string Name
        {
            get => name; set
            { 
                Set(ref name, value);
                SendCommand.RaiseCanExecuteChanged();
            }
        }
        public PAnalysisVM(Messenger messenger,IRepository<Analysis> anaRep, IRepository<Patient> patRep)
        {
            this.anaRep = anaRep;
            this.patRep = patRep;
            this.messenger = messenger;
            this.messenger.Register<CurrentPatientChange>(this, x =>
            {
                Patient = x.CrPatient;
            }, true);
        }
    }
}
