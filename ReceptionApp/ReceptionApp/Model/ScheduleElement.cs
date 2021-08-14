using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
{
    public class ScheduleElement
    {
        public string DayOfWeek { get; set; }
        public bool OffDay { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan BreakBeginTime { get; set; }
        public TimeSpan BreakEndTime { get; set; }        
    }
}
