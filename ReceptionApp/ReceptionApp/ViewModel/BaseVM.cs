using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class BaseVM
    {
        public BaseVM CurrentViewModel { get; set; }
    }
}
