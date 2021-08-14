using DoctorApp.Messenging;
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoctorApp.ViewModels
{
    class HistoryVM : ViewModelBase
    {
        private ObservableCollection<Patient> patients;

        public ObservableCollection<Patient> Patients { get => patients; set => Set(ref patients, value); }
        private Messenger messenger;

        public HistoryVM(Messenger messenger)
        {
            this.messenger = messenger;
            this.messenger.Register<CurrentProssChange>(this, x =>
            {

            }, true);
        }



    }
}
