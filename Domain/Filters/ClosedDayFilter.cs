namespace Domain.Filters;

public record ClosedDayFilter : BaseFilter
{
    public int OwnerId { get; set; }
    public DateTime? Date { get; set; } 
    public string? Reason { get; set; } // Причина закрытия
}