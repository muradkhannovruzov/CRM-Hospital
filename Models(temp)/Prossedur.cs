using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
   public enum ProssedurCategories
    {
        CheckUp,Operation,Diagnostic, Therapeutic, Rehabilitative
            //ve s.
    }
  

   public  class Prossedur
    {
        public string Name { get; set; }
        public Room Room { get; set; }
        public decimal Price { get; set; }
        public ProssedurCategories Categoria { get; set; }
        public DateTime Date { get; set; }
        public string DuringTime { get; set; }
        public Device UsingDevice { get; set; }
        public StringBuilder Results { get; set; }
        public List<Medicine> UsingMedicine { get; set; }
        public List<Medicine> Recept { get; set; }

    }
}
