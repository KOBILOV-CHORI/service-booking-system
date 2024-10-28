namespace Domain.Dtos;

public record SpecialBreakUpdateDto : BaseSpecialBreakDto
{
    public int Id { get; set; }
}

public record SpecialBreakCreateDto : BaseSpecialBreakDto {}

public record SpecialBreakReadDto : BaseSpecialBreakDto
{
    public int Id { get; set; }
}

public record BaseSpecialBreakDto
{
    public int WorkingHoursId { get; set; }

    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; } 
}