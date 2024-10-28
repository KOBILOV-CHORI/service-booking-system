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

        return PaginationResponse<IEnumerable<AppointmentReadDto>>.Create(filter.PageNumber, filter.PageSize,
            totalRecords, result);
    }

    public AppointmentReadDto? GetAppointmentById(int id)
    {
        return context.Appointments.Where(x => x.IsDeleted == false && x.Id == id)
            .Select(x => x.AppointmentToReadDto())
            .FirstOrDefault();
    }

    public bool CreateAppointment(AppointmentCreateDto createDto)
    {
        var client = context.Users.FirstOrDefault(c => c.Id == createDto.ClientId && !c.IsDeleted);
        if (client == null)
        {
            return false;
        }
        if (client.Role == UserRole.User)
        {
            client.Role = UserRole.Client;
            context.Users.Update(client); 
        }

        var service = context.Services.FirstOrDefault(s => s.Id == createDto.ServiceId && !s.IsDeleted);
        if (service == null)
        {
            return false;
        }

        var owner = service.Owner;
        var workingHours =
            context.WorkingHours.FirstOrDefault(wh =>
                wh.OwnerId == owner.Id && wh.Day == createDto.StartTime.DayOfWeek);

        if (workingHours == null || createDto.StartTime < workingHours.StartTime ||
            createDto.EndTime > workingHours.EndTime)
        {
            return false;
        }

        var breaks = context.Breaks.Where(b => b.WorkingHoursId == workingHours.Id).ToList();
        foreach (var b in breaks)
        {
            if ((createDto.StartTime >= b.StartTime && createDto.StartTime < b.EndTime) ||
                (createDto.EndTime > b.StartTime && createDto.EndTime <= b.EndTime))
            {
                return false;
            }
        }

        bool hasConflict = context.Appointments.Any(a =>
            a.ServiceId == createDto.ServiceId &&
            a.StartTime < createDto.EndTime &&
            a.EndTime > createDto.StartTime &&
            !a.IsDeleted);

        if (hasConflict)
        {
            return false;
        }

        context.Appointments.Add(createDto.CreateDtoToAppointment());
        context.SaveChanges();
        return true;
    }


    public bool UpdateAppointment(AppointmentUpdateDto updateDto)
    {
        var existingAppointment = context.Appointments.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingAppointment == null) return false;

        var client = context.Users.FirstOrDefault(c => c.Id == updateDto.ClientId && !c.IsDeleted);
        if (client == null)
        {
            return false;
        }

        var service = context.Services.FirstOrDefault(s => s.Id == updateDto.ServiceId && !s.IsDeleted);
        if (service == null)
        {
            return false;
        }

        var owner = service.Owner;
        var workingHours =
            context.WorkingHours.FirstOrDefault(wh =>
                wh.OwnerId == owner.Id && wh.Day == updateDto.StartTime.DayOfWeek);

        if (workingHours == null || updateDto.StartTime < workingHours.StartTime ||
            updateDto.EndTime > workingHours.EndTime)
        {
            return false;
        }

        bool isTimeConflict = context.Appointments.Any(a =>
            a.ServiceId == updateDto.ServiceId &&
            a.Id != updateDto.Id &&
            !a.IsDeleted &&
            ((updateDto.StartTime >= a.StartTime && updateDto.StartTime < a.EndTime) ||
             (updateDto.EndTime > a.StartTime && updateDto.EndTime <= a.EndTime) ||
             (updateDto.StartTime <= a.StartTime && updateDto.EndTime >= a.EndTime))
        );

        if (isTimeConflict)
        {
            return false;
        }

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