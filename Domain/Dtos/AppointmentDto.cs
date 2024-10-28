using Domain.Entities;

namespace Domain.Dtos;

public record AppointmentUpdateDto : BaseAppointmentDto
{
    public int Id { get; set; }
}

public record AppointmentCreateDto : BaseAppointmentDto {}

public record AppointmentReadDto : BaseAppointmentDto
{
    public int Id { get; set; }
}

public record BaseAppointmentDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }

    public int ClientId { get; set; }
    public int ServiceId { get; set; }
}