using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.ViewModels
{
    class PDiagnosisVM : ViewModelBase
    {
        private Messenger messenger;
        private IRepository<Prossedur> proRep;
        private string name;
        private RelayCommand okCommand;

        public RelayCommand OkCommand => okCommand ?? (okCommand = new RelayCommand(() =>
        {
            Prossedur.Diagnosis = Text;
            proRep.Update(x => { return x.Id == Prossedur.Id; }, Prossedur);
            Text = string.Empty;
        }, () => !string.IsNullOrWhiteSpace(Text)));

        public Prossedur Prossedur { get; set; }
        public string Text
        {
            get => name; set
            {
                Set(ref name, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }
        public PDiagnosisVM(Messenger messenger, IRepository<Prossedur> proRep)
        {
            this.proRep = proRep;
            this.messenger = messenger;
            this.messenger.Register<CurrentProssChange>(this, x =>
            {
                Prossedur = x.CurrentProssedur;
            }, true);
        }
    }
}
