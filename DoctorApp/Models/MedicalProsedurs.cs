using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Models
{
    public class MedicalProsedurs
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
