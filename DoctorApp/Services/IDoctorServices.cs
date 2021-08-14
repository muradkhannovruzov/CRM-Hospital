using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Services
{
    public interface IDoctorServices
    {
        Doctor FindDoctor(string username, string password);
    }
}
