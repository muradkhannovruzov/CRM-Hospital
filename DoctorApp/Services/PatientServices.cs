using DoctorApp.Models;
using DoctorApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DoctorApp.Services
{
    public class PatientServices : IPatientServices
    {
        private IRepository<Patient> repository;
        public PatientServices(IRepository<Patient> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Patient> FindPatient(IEnumerable<string> ids)
        {
            var patients = repository.GetAll();
            var myPatients = new ObservableCollection<Patient>();
            foreach (var item in ids)
            {
                var patient = patients.FirstOrDefault(x => x.Id == item);
                if (patient != null)
                    myPatients.Add(patient);
                
            }
            return myPatients;
        }

        public Patient FindPatient(string id)
        {
            var patients = repository.GetAll();
            return patients.FirstOrDefault(x => x.Id == id);
        }
    }
}
