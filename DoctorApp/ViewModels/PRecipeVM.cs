using DoctorApp.Models;
using DoctorApp.Services;
using DoctorApp.Messenging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoctorApp.ViewModels
{
    class PRecipeVM : ViewModelBase
    {
        private IProssedurServices prossedurServices;
        private Messenger messanger;
        public Prossedur Prossedur { get; set; }
        public PRecipeVM(Messenger messenger, IProssedurServices prossedurServices)
        {
            this.messanger = messenger;
            this.prossedurServices = prossedurServices;
            this.messanger.Register<CurrentProssChange>(this, x =>
            {
                Recipe = x.CurrentProssedur?.Recept;
            },true);

        }
        private ObservableCollection<ReceptElement> recipe;
        public ObservableCollection<ReceptElement> Recipe { get => recipe; set => Set(ref recipe, value); }
    }
}
