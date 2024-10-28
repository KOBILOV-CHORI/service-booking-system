using Domain.Entities;

namespace Domain.Filters;

public record PaymentFilter : BaseFilter
{
    public int? AppointmentId { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public DateTime? MinPaymentDate { get; set; }
    public DateTime? MaxPaymentDate { get; set; }
    public PaymentStatus? Status { get; set; }
}