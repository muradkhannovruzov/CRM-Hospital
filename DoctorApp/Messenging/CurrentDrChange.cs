using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Messenging
{
    public class CurrentDrChange : IMessage
    {
        public Doctor CurrnetDr { get; set; }
    }
}