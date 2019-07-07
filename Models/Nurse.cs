namespace ApteanClinic.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        public User NurseUser { get; set; }
        public bool Availability { get; set; }
    }
}