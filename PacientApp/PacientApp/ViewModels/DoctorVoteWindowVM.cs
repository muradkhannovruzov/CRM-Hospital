using DoctorApp.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HealthCare.Repository;
using HealthCare.Service;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PacientApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DoctorVoteWindowVM: ViewModelBase
    {
        public Prossedur CurrentProssedure { get; set; }
        public int Vote { get; set; }
        public IRepository<Doctor> DoctorRepo { get; set; }
        public FileService fileServise { get; set; }
        public Patient CurrentUser { get; set; }
        Doctor currentDoctor;
        public ICommand SaveCommand { get; set; }
        public DoctorVoteWindowVM(Patient currentUser,Prossedur prossedur)
        {
            CurrentProssedure = prossedur;
            CurrentUser = currentUser;

            CurrentProssedure.Room.IsEmty = true;
            fileServise = new FileService();
            DoctorRepo = new Repository<Doctor>(fileServise);
            SaveCommand = new RelayCommand(()=>
            { 
            var docs = DoctorRepo.GetAll().ToList();
            if (docs!=null)
            {
                foreach (var item in docs)
                {
                    if (item.Name==CurrentProssedure.DrName)
                    {
                        currentDoctor = item;
                    }
                }
            }
            if (currentDoctor!=null)
            {
                var vote = new Vote();
                vote.StarPoint = Vote;
                vote.Username = CurrentUser.Username;
                vote.Mail = CurrentUser.Mail;
                currentDoctor.Raiting.AddVote(vote);
                DoctorRepo.Update(x => { return x.Id == currentDoctor.Id; },currentDoctor);
               
                
            }
            });
        }
    }
}
