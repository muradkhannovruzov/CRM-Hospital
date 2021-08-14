using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace DoctorApp.Models
{
    public enum ProssedurCategories
    {
        CheckUp, Operation, Diagnostic, Therapeutic, Rehabilitative        //ve s.
    }
    public class Prossedur
    {
        public string Id { get; set; }
        public string PatientDescription { get; set; }
        public string DrName { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientId { get; set; }
        public Room Room { get; set; }
        public decimal Price { get; set; }
        public decimal DoctorProfit { get; set; }
        public ProssedurCategories Categoria { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public List<Device> UsingDevice { get; set; } = new List<Device>();
        public StringBuilder Results { get; set; }
        public List<Medicine> UsingMedicine { get; set; } = new List<Medicine>();
        public ObservableCollection<ReceptElement> Recept { get; set; } = new ObservableCollection<ReceptElement>();
        public Status Status { get; set; }
        public string Diagnosis { get; set; }

        public Prossedur()
        {
            Id = Guid.NewGuid().ToString();
            Status = Status.NotCame;
        }

        public override string ToString()
        {
            return "    " + PatientName + "\n" + DateBegin.Day.ToString() + ":" + DateBegin.Hour.ToString() + ":" + DateBegin.Minute.ToString() + "\n" + DateEnd.Day.ToString() + ":" + DateEnd.Hour.ToString() + ":" + DateEnd.Minute.ToString() + "\n" + Categoria.ToString();
        }
    }

    public class ReceptElement
    {
        public string MedicineName { get; set; }
        public string Note { get; set; }
    }

    public enum Status
    {
        NotCame,
        HasCame,
        Waiting,
        InProcess,
        End,
        Canceled
    }
}
