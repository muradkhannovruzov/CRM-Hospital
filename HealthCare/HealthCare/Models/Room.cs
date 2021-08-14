namespace HealthCare.Models
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
        public TypeOfRoom RoomType { get; set; }
        public bool IsEmty { get; set; } = true;
    }
}
