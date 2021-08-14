using DoctorApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorApp.Models;

namespace DoctorApp.Services
{
    public interface IProssedurServices
    {
        IEnumerable<Prossedur> FindProssedurs(IEnumerable<string> ProsedurID);
        IEnumerable<Prossedur> FindSpecificDayProssedurs(IEnumerable<string> ProsedurID, DateTime date);
        Prossedur FindProssedurs(string id);
    }
}
