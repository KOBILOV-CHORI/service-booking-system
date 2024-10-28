namespace Domain.Filters;

public record SpecialBreakFilter : BaseFilter
{
    public int? WorkingHoursId { get; set; }   
    public DateTime? StartTime { get; set; }   
    public DateTime? EndTime { get; set; }
}