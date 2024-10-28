using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.PaymentServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/payments")]
public sealed class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPayments([FromQuery] PaymentFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<PaymentReadDto>>>.Success(null,
            paymentService.GetAllPayments(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetPaymentById(int id)
    {
        PaymentReadDto? res = paymentService.GetPaymentById(id);
        return res != null
            ? Ok(ApiResponse<PaymentReadDto?>.Success(null, res))
            : NotFound(ApiResponse<PaymentReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreatePayment([FromBody] PaymentCreateDto paymentCreateDto)
    {
        PaymentCreateDto info = paymentCreateDto;
        bool res = paymentService.CreatePayment(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdatePayment(PaymentUpdateDto info)
    {
        bool res = paymentService.UpdatePayment(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePayment(int id)
    {
        bool res = paymentService.DeletePayment(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}