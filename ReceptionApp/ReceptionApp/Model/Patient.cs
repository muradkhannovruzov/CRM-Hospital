using System;
using System.Collections.Generic;
using System.Text;

namespace ReceptionApp.Model
{
    public enum Status
    {
        NotCame,
        HasCame,
        Waiting,
        InProcess,
        End,
        Canceled
    }
    public class Patient : Human
    {
        public HistoryBook HistoryBook { get; set; } = new HistoryBook();
        public List<string> ProssedursId { get; set; } = new List<string>();
        public List<string> CurrentAnalysesId { get; set; } = new List<string>();
        public List<string> DoctorRequestId { get; set; } = new List<string>();
        public Raiting Raiting { get; set; } = new Raiting();
    }
}
