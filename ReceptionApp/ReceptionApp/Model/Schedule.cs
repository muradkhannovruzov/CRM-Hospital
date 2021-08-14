using System.Collections.ObjectModel;

namespace ReceptionApp.Model
{
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
}
