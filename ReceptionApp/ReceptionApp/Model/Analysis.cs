using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReceptionApp.Model
{
    public class Analysis
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ObservableCollection<AnalysisElement> GetAnalys { get; set; } = new ObservableCollection<AnalysisElement>();
    }
}
