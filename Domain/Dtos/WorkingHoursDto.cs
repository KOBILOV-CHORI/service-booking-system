namespace Domain.Dtos;

public record WorkingHoursUpdateDto : BaseWorkingHoursDto
{
    public int Id { get; set; }
}

public record WorkingHoursCreateDto : BaseWorkingHoursDto {}

public record WorkingHoursReadDto : BaseWorkingHoursDto
{
    public int Id { get; set; }
}

public record BaseWorkingHoursDto
{
    public int OwnerId { get; set; }    
    public DayOfWeek Day { get; set; } 
    public DateTime StartTime { get; set; } 
    public DateTime EndTime { get; set; } 
}