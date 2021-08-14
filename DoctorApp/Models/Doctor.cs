using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoctorApp.Models
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
        //ve s.    
    }


    public class Doctor : Human
    {
        public DoctorCategories Categoria { get; set; }
        public Room DefaultRoom { get; set; } = new Room();
        public ObservableCollection<string> ProssedurId { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> OldProssedurId { get; set; } = new ObservableCollection<string>();
        public Raiting Raiting { get; set; } = new Raiting();
        public ObservableCollection<string> Pacients { get; set; } = new ObservableCollection<string>();
        public List<OldPatient> OldPacients { get; set; } = new List<OldPatient>();
        public ObservableCollection<DoctorCanMake> DoctorCanMake { get; set; } = new ObservableCollection<DoctorCanMake>();
        public double MainProfitPercent { get; set; } = 0;
        public Schedule Schedule { get; set; } = new Schedule();
    }

    public class DoctorCanMake
    {
        public string NameOfProsedur { get; set; }
        public ObservableCollection<Medicine> UsingMedicine { get; set; } = new ObservableCollection<Medicine>();
        public ObservableCollection<Device> UsingDevice { get; set; } = new ObservableCollection<Device>();
        public string TimeOfProsedur { get; set; }
        public decimal Price { get; set; }
        public Room Room { get; set; } = new Room();
    }

    public class OldPatient
    {
        public string Name { get; set; }
        public double Money { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class Schedule
    {
        public ObservableCollection<ScheduleElement> ScheduleElements { get; set; } = new ObservableCollection<ScheduleElement>();

        public Schedule()
        {
            if (ScheduleElements.Count < 2)
            {
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Monday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Tuesday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Wednesday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Thursday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Friday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Saturday" });
                ScheduleElements.Add(new ScheduleElement() { DayOfWeek = "Sunday" });
            }
        }
    }


    public class ScheduleElement
    {
        public string DayOfWeek { get; set; }
        public bool OffDay { get; set; } = false;
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakBeginTime { get; set; }
        public TimeSpan BreakEndTime { get; set; }
        
    }

    public class Raiting
    {
        public double Score { get; private set; }
        public ObservableCollection<Vote> votes;
        public Raiting()
        {
            votes = new ObservableCollection<Vote>();
        }
        public void AddVote(Vote vote)
        {
            votes.Add(vote);
            Score = votes.Average(x => x.StarPoint);
        }

    }

    public class Vote
    {
        public int StarPoint { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Note { get; set; }
    }
}
