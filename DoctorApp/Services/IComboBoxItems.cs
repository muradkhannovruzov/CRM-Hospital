using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoctorApp.Services
{
    interface IComboBoxItems
    {
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<string> GetTimeAndDoze();
        public ObservableCollection<string> GetHours();
        public ObservableCollection<string> GetMinutes();
    }
}
