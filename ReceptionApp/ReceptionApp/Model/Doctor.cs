using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ReceptionApp.Model
{
    public enum DoctorCategories
    {
        [Description("Dentist")]
        Dentist,
        [Description("Surgeon")]
        Surgeon,
        [Description("Cardiologist")]
        Cardiologist,
        [Description("Pediatrician")]
        Pediatrician,
        [Description("Gynecologist")]
        Gynecologist,
        [Description("Psychiatrist")]
        Psychiatrist,
        [Description("Dermatologist")]
        Dermatologist,
        [Description("Endocrinologist")]
        Endocrinologist,
        [Description("Gastroenterologist")]
        Gastroenterologist,
        [Description("Nephrologist")]
        Nephrologist,
        [Description("Oncologist")]
        Oncologist
    }
    
    public class Doctor : Human
    {
        public DoctorCategories Categoria { get; set; }
        public Room DefaultRoom { get; set; } = new Room();
        public ObservableCollection<string> ProssedurId { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> OldProssedurId { get; set; } = new ObservableCollection<string>();
        public Raiting Raiting { get; set; } = new Raiting();
        public ObservableCollection<string> Pacients { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<OldPatient> OldPacients { get; set; } = new ObservableCollection<OldPatient>();
        public ObservableCollection<DoctorCanMake> DoctorCanMake { get; set; } = new ObservableCollection<DoctorCanMake>();
        public double MainProfitPercent { get; set; } = 0;
        public Schedule Schedule { get; set; } = new Schedule();
    }
}
