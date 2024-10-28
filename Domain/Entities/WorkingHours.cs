namespace Domain.Entities;

public class WorkingHours : BaseEntity
{
    public int OwnerId { get; set; }
    public User Owner { get; set; } // Используем User с Role = Owner

    public DayOfWeek Day { get; set; } 
    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; } 

    public List<Break> Breaks { get; set; } = new List<Break>(); 
    public List<SpecialBreak> SpecialBreaks { get; set; } = new List<SpecialBreak>();
}