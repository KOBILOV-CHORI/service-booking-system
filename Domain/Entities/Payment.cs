namespace Domain.Entities;

public class Payment : BaseEntity
{
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
}