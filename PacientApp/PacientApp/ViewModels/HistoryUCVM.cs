
using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HealthCare.Repository;
using HealthCare.Service;
using PacientApp.Views;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HistoryUCVM : ViewModelBase
        {
            private IMessenger messenger;
            private IFileService FileService { get; set; }
            private Patient CurrentUser { get; set; }
            private IRepository<Patient> PatientRepository { get; set; }
            private IRepository<Prossedur> ProssedursRepository { get; set; }
            public Prossedur SelectedProssedur { get; set ; }
            public ObservableCollection<Prossedur> AllProssedurs { get; set; }
            public ObservableCollection<Prossedur> TodayProssedurs { get; set; }
            public ICommand SelectedProssedurChanged { get; set; }
            public IRepository<Prossedur> ProssedurRepo { get; set; }
            public IRepository<Doctor> DoctorRepo { get; set; }

        public HistoryUCVM(IRepository<Patient> repository, IFileService fileService, IMessenger messenger, Patient patient)
            {
                this.messenger = messenger;
                CurrentUser = patient;
                FileService = fileService;
                PatientRepository = repository;
                ProssedurRepo = new Repository<Prossedur>(fileService);
                GetCurrentUserAllProceddurs();
                GetCurrentUserTodayProceddurs();
            SelectedProssedurChanged = new RelayCommand(() =>
            {
                if (SelectedProssedur!=null)
                {

                    if (SelectedProssedur.Status==Status.Canceled)
                    {
                        ProssedurRepo.Remove(x => { return x.Id == SelectedProssedur.Id; });

                    }
                    else if (SelectedProssedur.Status == Status.End)
                    {
                        var vote = new DoctorVoteUC(CurrentUser ,SelectedProssedur);
                        vote.ShowDialog();
                        ProssedurRepo.Remove(x => { return x.Id == SelectedProssedur.Id; });
                    }
                    else
                        ProssedurRepo.Update(x => { return x.Id == SelectedProssedur.Id; }, SelectedProssedur);
                        //GetCurrentUserAllProceddurs();
                        //GetCurrentUserTodayProceddurs();
                }
            }
            );

        }

        public void GetCurrentUserAllProceddurs()
            {
                ProssedursRepository = new Repository<Prossedur>(FileService);
                var allProssedurs = ProssedursRepository.GetAll();
                AllProssedurs = new ObservableCollection<Prossedur>();
            if (allProssedurs!=null)
            {
                foreach (var allProssedurItem in allProssedurs)
                {
                    foreach (var UserProssedurItem in CurrentUser.ProssedursId)
                    {
                        if (allProssedurItem.Id == UserProssedurItem)
                        {
                        AllProssedurs.Add(allProssedurItem);
                        }

                    }

                }

            }
            }
            public void GetCurrentUserTodayProceddurs()
        {
            if (AllProssedurs.Count>0)
            {
                ProssedursRepository = new Repository<Prossedur>(FileService);
                TodayProssedurs = new ObservableCollection<Prossedur>();
                
                foreach (var item in AllProssedurs)
                {
                    if (item.DateBegin.Date== DateTime.Now.Date )
                    {
                        TodayProssedurs.Add(item);
                        
                    }

                }
               
            }
        }
            
    }
    
}
