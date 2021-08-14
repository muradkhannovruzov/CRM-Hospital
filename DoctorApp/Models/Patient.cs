using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Models
{
    public class Patient : Human
    {
        public HistoryBook HistoryBook { get; set; } = new HistoryBook();
        public List<string> ProssedursId { get; set; } = new List<string>();
        public List<string> CurrentAnalysesId { get; set; } = new List<string>();
        public List<string> DoctorRequestId { get; set; } = new List<string>();
        public Raiting Raiting { get; set; } = new Raiting();
    }


    public class DoctorRequest
    {
        public string Id { get; set; }
        public ProssedurCategories Category { get; set; }
        public string DocName { get; set; }
        public DateTime RequestTime { get; set; }
        public DoctorRequest()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
