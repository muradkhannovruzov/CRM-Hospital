using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReceptionApp.Model
{
    public class DoctorCanMake
    {
        public string NameOfProsedur { get; set; }
        public ObservableCollection<Medicine> UsingMedicine { get; set; } = new ObservableCollection<Medicine>();
        public ObservableCollection<string> UsingDevice { get; set; } = new ObservableCollection<string>();
        public string TimeOfProsedur { get; set; }
        public decimal Price { get; set; }
        public Room Room { get; set; } = new Room();
    }
}
