using DoctorApp.Models;
using DoctorApp.Repository;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DoctorApp.Services
{
    public class ProssedurServices : IProssedurServices
    {
        private IRepository<Prossedur> repository;
        public ProssedurServices(IRepository<Prossedur> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Prossedur> FindProssedurs(IEnumerable<string> ProssedurID)
        {
            var prossedurs = repository.GetAll();
            var myProssedurs = new ObservableCollection<Prossedur>();
            foreach (var item in ProssedurID)
            {
                var prossedur = prossedurs.FirstOrDefault(x => x.Id == item);
                if(prossedur != null)
                    myProssedurs.Add(prossedur);
            }
            return BoobleSort(myProssedurs);
        }

        public Prossedur FindProssedurs(string id)
        {
            var prossedurs = repository.GetAll();
            return prossedurs.FirstOrDefault(x => x.Id == id);
        }

        private IEnumerable<Prossedur> BoobleSort(ObservableCollection<Prossedur> prossedurs)
        {
            for (int i = 0; i < prossedurs.Count; i++)
            {
                for (int j = 0; j < prossedurs.Count - i -1; j++)
                {
                    if(prossedurs[j].DateBegin > prossedurs[j+1].DateBegin)
                    {
                        var temp = prossedurs[j];
                        prossedurs[j] = prossedurs[j+1];
                        prossedurs[j+1] = temp;
                    }
                }
            }
            return prossedurs;
        }

        public IEnumerable<Prossedur> FindSpecificDayProssedurs(IEnumerable<string> ProssedurID, DateTime date)
        {
            var prossedurs = repository.GetAll();
            var myProssedurs = new ObservableCollection<Prossedur>();
            foreach (var item in ProssedurID)
            {
                var prossedur = prossedurs.FirstOrDefault(x => x.Id == item);
                if (prossedur != null)
                    if(prossedur.DateBegin.Date == date.Date)myProssedurs.Add(prossedur);
            }
            
            return BoobleSort(myProssedurs);
        }
    }
}
