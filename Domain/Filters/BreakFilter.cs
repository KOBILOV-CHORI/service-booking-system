namespace Domain.Filters;

public record BreakFilter : BaseFilter
{
    public int? WorkingHoursId { get; set; }
    public DateTime? StartTime { get; set; } 
    public DateTime? EndTime { get; set; }
}