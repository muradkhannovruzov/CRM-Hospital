using DoctorApp.Messenging;
using DoctorApp.Models;
using DoctorApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class LoginVM : ViewModelBase
    {
        private string usernameText;
        private string passwordText="";
        private RelayCommand loginCommand;
        private Messenger messenger;
        private IDoctorServices doctorServices;
        private Visibility incorrect = Visibility.Hidden;

        public Visibility Incorrect { get => incorrect; set =>  Set(ref incorrect, value); }
        public LoginVM(Messenger messenger, IDoctorServices doctorServices)
        {
            this.messenger = messenger;
            this.doctorServices = doctorServices;
        }
        public string PasswordText { get => passwordText; set => Set(ref passwordText, value); }
        public string UsernameText { get => usernameText; set => Set(ref usernameText, value); }

        public RelayCommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand(() =>
        {
            var doc = doctorServices.FindDoctor(UsernameText, PasswordText);
            if (doc != null)
            {
                if (doc.Schedule.ScheduleElements.Count > 7)
                {
                    int a = doc.Schedule.ScheduleElements.Count;
                    for (int i = 0; i < a-7; i++)
                    {
                        doc.Schedule.ScheduleElements.RemoveAt(0);
                    }
                }
                messenger.Send(new CurrentDrChange() { CurrnetDr = doc });
                messenger.Send(new ViewChange() { ViewModel = App.Container.GetInstance<HomeVM>() });
                Incorrect = Visibility.Hidden;
            }
            else Incorrect = Visibility.Visible;
        }));



    }
}
