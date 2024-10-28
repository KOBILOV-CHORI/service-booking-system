namespace Domain.Filters;

public record WorkingHoursFilter : BaseFilter
{
    public int? OwnerId { get; set; }
    public DayOfWeek? Day { get; set; } 
    public DateTime? StartTime { get; set; } 
    public DateTime? EndTime { get; set; } 
}