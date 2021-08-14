using System.Collections.Generic;

namespace DoctorApp.Models
{
    public class HistoryBook
    {
        public List<string> AnalyseResultsId { get; set; } = new List<string>();
        public List<string> ProssedursId { get; set; } = new List<string>();
    }
}
