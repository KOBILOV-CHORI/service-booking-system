using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.AppointmentServices;

public class AppointmentService(DataContext context) : IAppointmentService
{
    public PaginationResponse<IEnumerable<AppointmentReadDto>> GetAllAppointments(AppointmentFilter filter)
    {
        IQueryable<Appointment> appointments = context.Appointments;
        if (filter.StartTime != null)
            appointments = appointments.Where(x => x.StartTime >= filter.StartTime);
        if (filter.EndTime != null)
            appointments = appointments.Where(x => x.EndTime <= filter.EndTime);
        if (filter.Status != null)
            appointments = appointments.Where(x => x.Status == filter.Status);
        if (filter.ClientId != null)
            appointments = appointments.Where(x => x.ClientId == filter.ClientId);
        if (filter.ServiceId != null)
            appointments = appointments.Where(x => x.ServiceId == filter.ServiceId);

        int totalRecords = appointments.Count();
        var result = appointments.Skip((filter.PageNumber - 1) * filter.PageSize)
                                 .Take(filter.PageSize)
                                 .Where(x => !x.IsDeleted)
                                 .Select(x => x.AppointmentToReadDto())
                                 .ToList();

        return PaginationResponse<IEnumerable<AppointmentReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public AppointmentReadDto? GetAppointmentById(int id)
    {
        return context.Appointments.Where(x => x.IsDeleted == false && x.Id == id)
                                   .Select(x => x.AppointmentToReadDto())
                                   .FirstOrDefault();
    }

    public bool CreateAppointment(AppointmentCreateDto createDto)
    {
        context.Appointments.Add(createDto.CreateDtoToAppointment());
        context.SaveChanges();
        return true;
    }

    public bool UpdateAppointment(AppointmentUpdateDto updateDto)
    {
        var existingAppointment = context.Appointments.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingAppointment == null) return false;

        existingAppointment.UpdateDtoToAppointment(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteAppointment(int id)
    {
        var existingAppointment = context.Appointments.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingAppointment == null) return false;

        existingAppointment.IsDeleted = true;
        existingAppointment.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
