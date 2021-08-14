using System;
using System.Collections.Generic;
using System.Text;


namespace DoctorApp.Models
{
    public class Device
    {
        public bool IsBusy { get; set; }
        public string Name { get; set; }
        public decimal Profit { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
