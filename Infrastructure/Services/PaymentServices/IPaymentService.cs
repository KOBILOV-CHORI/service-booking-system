using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.PaymentServices;

public interface IPaymentService
{
    PaginationResponse<IEnumerable<PaymentReadDto>> GetAllPayments(PaymentFilter filter);
    PaymentReadDto? GetPaymentById(int id);
    bool CreatePayment(PaymentCreateDto createDto);
    bool UpdatePayment(PaymentUpdateDto updateDto);
    bool DeletePayment(int id);
}