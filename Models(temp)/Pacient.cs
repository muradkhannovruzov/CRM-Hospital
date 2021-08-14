using System;
using System.Collections.Generic;
using System.Text;

namespace Demo
{
    public class Pacient:Human
    {
        public HistoryBook HistoryBook { get; set; }
        public List<Prossedur> CurrentProssedurs { get; set; }
        public List<Analys> CurrentAnalyses { get; set; }

    }
}
