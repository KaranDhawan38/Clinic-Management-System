namespace ApteanClinic.Models
{
    public class DoctorTime
    {
        public int Id { get; set; }
        public int TimeId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
