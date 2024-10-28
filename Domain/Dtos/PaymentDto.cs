using Domain.Entities;

namespace Domain.Dtos;

public record PaymentUpdateDto : BasePaymentDto
{
    public int Id { get; set; }
}

public record PaymentCreateDto : BasePaymentDto {}

public record PaymentReadDto : BasePaymentDto
{
    public int Id { get; set; }
}

public record BasePaymentDto
{
    public int AppointmentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }
}