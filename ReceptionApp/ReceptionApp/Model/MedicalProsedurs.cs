using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
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
