namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }

    public int ClientId { get; set; }
    public User Client { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public List<Payment> Payments { get; set; } = new List<Payment>();
}