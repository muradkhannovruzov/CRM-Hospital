using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Services
{
    public interface IPatientServices
    {
        IEnumerable<Patient> FindPatient(IEnumerable<string> ids);
        Patient FindPatient(string id);
    }
}
