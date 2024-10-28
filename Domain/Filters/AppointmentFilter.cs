using Domain.Entities;

namespace Domain.Filters;

public record AppointmentFilter : BaseFilter
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public AppointmentStatus? Status { get; set; }
    public int? ClientId { get; set; }
    public int? ServiceId { get; set; }
}