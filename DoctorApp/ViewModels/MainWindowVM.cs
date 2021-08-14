using DoctorApp.Messenging;
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DoctorApp.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private bool y;
        public int Height { get => height; set => Set(ref height, value); }
        public int Width { get => width; set {
                Row1Width = value - Row0Width;
                Set(ref width, value);
            }

        }
        private ViewModelBase currentView;
        private RelayCommand goHome;
        private RelayCommand goSchedule;
        private RelayCommand goProfile;
        private string nameAndSurname;
        private string rating;
        private string category;
        private RelayCommand goLogin;
        private RelayCommand goProcess;
        private RelayCommand goProsedurs;
        private int height = 580;
        private int width = 900;
        private RelayCommand goHistory;
        private int widthOfMainStack = 46;
        private RelayCommand makeBigger;
        private int row0Width = 90;
        private int row1Width ;

        public int WidthOfMainStack { get => widthOfMainStack; set => Set(ref widthOfMainStack, value); }
        public Doctor Doc { get; set; }
        public string NameAndSurname { get => nameAndSurname; set => Set(ref nameAndSurname, value); }
        public string Rating { get => rating; set => Set(ref rating, value); }
        public string Category { get => category; set => Set(ref category, value); }
        public int Row0Width { get => row0Width; set => Set(ref row0Width, value); }
        public int Row1Width { get => row1Width; set
            {
                Set(ref row1Width, value);
            }
        }
        public ViewModelBase CurrentView
        {
            get => currentView;
            set
            {
                Set(ref currentView, value);
                if (y)
                {
                    GoSchedule.RaiseCanExecuteChanged();
                    GoHome.RaiseCanExecuteChanged();
                    GoProfile.RaiseCanExecuteChanged();
                    GoLogin.RaiseCanExecuteChanged();
                    GoProcess.RaiseCanExecuteChanged();
                    GoProsedurs.RaiseCanExecuteChanged();
                    GoHistory.RaiseCanExecuteChanged();
                    MakeBigger.RaiseCanExecuteChanged();
                }
            }
        }
        public MainWindowVM()
        {
            var messenger = App.Container.GetInstance<Messenger>();
            messenger.Register<CurrentDrChange>(this, message =>
            {
                Doc = message.CurrnetDr;
                NameAndSurname = Doc.Name + " " + Doc.Surname;
                Rating = Doc.Raiting.Score.ToString();
                Category = Doc.Categoria.ToString();
            });

            CurrentView = App.Container.GetInstance<LoginVM>();
            messenger.Register<ViewChange>(this, x =>
            {
                y = x.x;
                CurrentView = x.ViewModel;
            });
        }
        public RelayCommand GoHome => goHome ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<HomeVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand GoHistory => goHistory ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<HistoryVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand GoSchedule => goSchedule ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<ScheduleVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand GoProfile => goProfile ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<ProfileVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand GoProcess => goProcess ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<ProcessVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand GoLogin => goLogin ??= new RelayCommand(() =>
        {
            CurrentView = App.Container.GetInstance<LoginVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());

        public RelayCommand GoProsedurs => goProsedurs ??= new RelayCommand(() =>
        { 
            CurrentView = App.Container.GetInstance<ProsedursVM>();
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
        public RelayCommand MakeBigger => makeBigger ??= new RelayCommand(() =>
        {
            if (WidthOfMainStack == 46)
            {
                WidthOfMainStack = 100;
                Row0Width = 100;
                Row1Width = Width - Row0Width;
            }
            else
            {
                WidthOfMainStack = 46;
                Row0Width = 46;
                Row1Width = Width - Row0Width;
            }
        }, () => CurrentView != App.Container.GetInstance<LoginVM>());
    }
}
