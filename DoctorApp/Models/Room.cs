using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorApp.Models
{
    public enum TypeOfRoom
    {
        Buffet,
        Dental,
        Surgical,
        Reanimation,
        Pediatric,
        Optometrist,
        Therapist,
        Cardiologist,
        Opersiya
    }
    public class Room
    {
        public int Number { get; set; }
        public bool IsEmpty { get; set; } = true;
        public TypeOfRoom RoomType { get; set; }
    }
}
