using PropertyChanged;
using ReceptionApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReceptionApp.Messages
{
    [AddINotifyPropertyChangedInterface]
    class NavigationMessage
    {
        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
    }
}
