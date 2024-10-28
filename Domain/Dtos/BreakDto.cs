namespace Domain.Dtos;

public record BreakUpdateDto : BaseBreakDto
{
    public int Id { get; set; }
}

public record BreakCreateDto : BaseBreakDto {}

public record BreakReadDto : BaseBreakDto
{
    public int Id { get; set; }
}

public record BaseBreakDto
{
    public int WorkingHoursId { get; set; }
    
    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; }
}