using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.AppointmentServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/appointments")]
public sealed class AppointmentController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAppointments([FromQuery] AppointmentFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<AppointmentReadDto>>>.Success(null,
            appointmentService.GetAllAppointments(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetAppointmentById(int id)
    {
        AppointmentReadDto? res = appointmentService.GetAppointmentById(id);
        return res != null
            ? Ok(ApiResponse<AppointmentReadDto?>.Success(null, res))
            : NotFound(ApiResponse<AppointmentReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateAppointment([FromBody] AppointmentCreateDto appointmentCreateDto)
    {
        AppointmentCreateDto info = appointmentCreateDto;
        bool res = appointmentService.CreateAppointment(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateAppointment(AppointmentUpdateDto info)
    {
        bool res = appointmentService.UpdateAppointment(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAppointment(int id)
    {
        bool res = appointmentService.DeleteAppointment(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}