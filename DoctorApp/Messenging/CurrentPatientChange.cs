using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Messenging
{
    public class CurrentPatientChange :IMessage
    {
        public Patient CrPatient { get; set; }
    }
}
