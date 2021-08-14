using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PacientApp.Messaging;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace PacientApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainVM : ViewModelBase
    {
        public ViewModelBase CurrentVM { get; set; }
        public IMessenger Messenger { get; set; }

        public MainVM()
        {
            CurrentVM = App.Container.GetInstance<LoginUCVM>();

            Messenger = App.Container.GetInstance<IMessenger>();

            Messenger.Register<VMChange>(this, x => CurrentVM = x.ViewModel);

        }

    }
}
