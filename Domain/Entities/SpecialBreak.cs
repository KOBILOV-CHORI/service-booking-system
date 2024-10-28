namespace Domain.Entities;

public class SpecialBreak : BaseEntity
{
    public int WorkingHoursId { get; set; }
    public WorkingHours WorkingHours { get; set; }

    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; } 
}