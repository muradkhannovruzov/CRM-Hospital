using DoctorApp.Models;
using DoctorApp.Repository;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DoctorApp.Services
{
    public class DoctorServices : IDoctorServices
    {
        private IRepository<Doctor> repository;
        public DoctorServices(IRepository<Doctor> repository)
        {
            this.repository = repository;
        }
        public Doctor FindDoctor(string username, string password)
        {
            var doctors = repository.GetAll();
            if (doctors != null) return doctors.FirstOrDefault(x => x.Username == username && x.Password == password);
            else return null;
        }
    }
}
