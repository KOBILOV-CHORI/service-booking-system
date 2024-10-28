using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.PaymentServices;

public class PaymentService(DataContext context) : IPaymentService
{
    public PaginationResponse<IEnumerable<PaymentReadDto>> GetAllPayments(PaymentFilter filter)
    {
        IQueryable<Payment> payments = context.Payments;
        if (filter.AppointmentId != null)
            payments = payments.Where(x => x.AppointmentId == filter.AppointmentId);
        if (filter.MinAmount != null)
            payments = payments.Where(x => x.Amount >= filter.MinAmount);
        if (filter.MaxAmount != null)
            payments = payments.Where(x => x.Amount <= filter.MaxAmount);
        if (filter.MinPaymentDate != null)
            payments = payments.Where(x => x.PaymentDate >= filter.MinPaymentDate);
        if (filter.MaxPaymentDate != null)
            payments = payments.Where(x => x.PaymentDate <= filter.MaxPaymentDate);
        if (filter.Status != null)
            payments = payments.Where(x => x.Status == filter.Status);

        int totalRecords = payments.Count();
        var result = payments.Skip((filter.PageNumber - 1) * filter.PageSize)
                             .Take(filter.PageSize)
                             .Where(x => !x.IsDeleted)
                             .Select(x => x.PaymentToReadDto())
                             .ToList();

        return PaginationResponse<IEnumerable<PaymentReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public PaymentReadDto? GetPaymentById(int id)
    {
        return context.Payments.Where(x => !x.IsDeleted && x.Id == id)
                               .Select(x => x.PaymentToReadDto())
                               .FirstOrDefault();
    }

    public bool CreatePayment(PaymentCreateDto createDto)
    {
        context.Payments.Add(createDto.CreateDtoToPayment());
        context.SaveChanges();
        return true;
    }

    public bool UpdatePayment(PaymentUpdateDto updateDto)
    {
        var existingPayment = context.Payments.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingPayment == null) return false;

        existingPayment.UpdateDtoToPayment(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeletePayment(int id)
    {
        var existingPayment = context.Payments.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingPayment == null) return false;

        existingPayment.IsDeleted = true;
        existingPayment.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
