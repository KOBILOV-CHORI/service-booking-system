using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.AppointmentServices;

public interface IAppointmentService
{
    PaginationResponse<IEnumerable<AppointmentReadDto>> GetAllAppointments(AppointmentFilter filter);    
    AppointmentReadDto? GetAppointmentById(int id);
    bool CreateAppointment(AppointmentCreateDto createDto);
    bool UpdateAppointment(AppointmentUpdateDto updateDto);
    bool DeleteAppointment(int id);
}