namespace Domain.Dtos;

public record ClosedDayUpdateDto : BaseClosedDayDto
{
    public int Id { get; set; }
}

public record ClosedDayCreateDto : BaseClosedDayDto {}

public record ClosedDayReadDto : BaseClosedDayDto
{
    public int Id { get; set; }
}

public record BaseClosedDayDto
{
    public int OwnerId { get; set; }
    public DateTime Date { get; set; } 
    public string Reason { get; set; } 
}