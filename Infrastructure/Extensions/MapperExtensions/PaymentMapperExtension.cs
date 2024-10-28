using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class PaymentMapperExtension
{
    public static PaymentReadDto PaymentToReadDto(this Payment payment)
    {
        return new PaymentReadDto()
        {
            Id = payment.Id,
            AppointmentId = payment.AppointmentId,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            Status = payment.Status
        };
    }

    public static Payment UpdateDtoToPayment(this Payment payment, PaymentUpdateDto updateDto)
    {
        payment.Amount = updateDto.Amount;
        payment.PaymentDate = updateDto.PaymentDate;
        payment.Status = updateDto.Status;
        payment.AppointmentId = updateDto.AppointmentId;
        payment.Version += 1;
        payment.UpdatedAt = DateTime.UtcNow;
        return payment;
    }

    public static Payment CreateDtoToPayment(this PaymentCreateDto createDto)
    {
        return new Payment()
        {
            Amount = createDto.Amount,
            PaymentDate = createDto.PaymentDate,
            Status = createDto.Status,
            AppointmentId = createDto.AppointmentId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Payment DeleteDtoToPayment(this Payment payment)
    {
        payment.IsDeleted = true;
        payment.DeletedAt = DateTime.UtcNow;
        payment.Version += 1;
        payment.UpdatedAt = DateTime.UtcNow;
        return payment;
    }
}