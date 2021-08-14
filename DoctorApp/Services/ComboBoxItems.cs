using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DoctorApp.Services
{
    class ComboBoxItems : IComboBoxItems
    {
        public ObservableCollection<string> Items { get ; set ; }

        public ObservableCollection<string> GetHours()
        {
            Items = new ObservableCollection<string>();
           Items.Add("01");
           Items.Add("02");
           Items.Add("03");
           Items.Add("04");
           Items.Add("05");
           Items.Add("06");
           Items.Add("07");
           Items.Add("08");
           Items.Add("09");
           Items.Add("10");
           Items.Add("11");
           Items.Add("12");
           Items.Add("13");
           Items.Add("14");
           Items.Add("15");
           Items.Add("16");
           Items.Add("17");
           Items.Add("18");
           Items.Add("19");
           Items.Add("20");
           Items.Add("21");
           Items.Add("22");
           Items.Add("23");
            Items.Add("00");
            return Items;
        }

        public ObservableCollection<string> GetMinutes()
        {
            Items = new ObservableCollection<string>();
            Items.Add("10");
            Items.Add("20");
            Items.Add("30");
            Items.Add("40");
            Items.Add("50");
            Items.Add("00");
            return Items;
        }

        public ObservableCollection<string> GetTimeAndDoze()
        {

            Items = new ObservableCollection<string>();
            Items.Add("1");
            Items.Add("2");
            Items.Add("3");
            Items.Add("4");
            Items.Add("5");
            Items.Add("6");
            Items.Add("7");
            Items.Add("8");
            Items.Add("9");
            Items.Add("10");
            return Items;
        }
    }
}
