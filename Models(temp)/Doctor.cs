using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public enum DoctorCategories
    {
       Dentist, Surgeon, Cardiologist, Pediatrician, Gynecologist, Psychiatrist, Dermatologist,Endocrinologist, Gastroenterologist, Nephrologist, Oncologist
            //ve s.
    }
    public enum Raiting
    {
        Low, Medium, High
    }
    public class Doctor:Human
    {
        public DoctorCategories Categoria { get; set; }
        public Room DefaultRoom { get; set; }
        public bool Available { get; set; }
        public Raiting Raiting{ get; set; }
        public List<Prossedur> Prossedurs { get; set; }
        public List<Pacient> Pacients { get; set; }
        public double MainProfitPercent { get; set; }

    }
}
