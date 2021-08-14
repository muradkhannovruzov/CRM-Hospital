using DoctorApp.Models;
using DoctorApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DoctorApp.Services
{
    class AnalysisServices : IAnalysisServices
    {
        private IRepository<Analysis> repository;
        public AnalysisServices(IRepository<Analysis> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Analysis> FindAnalyses(IEnumerable<string> AnalysisID)
        {
            var analyses = repository.GetAll();
            var myAnalyses = new ObservableCollection<Analysis>();
            foreach (var item in AnalysisID)
            {
                var prossedur = analyses.FirstOrDefault(x => x.Id == item);
                if (prossedur != null)
                    myAnalyses.Add(prossedur);
            }
            return myAnalyses;
        }
    }
}
