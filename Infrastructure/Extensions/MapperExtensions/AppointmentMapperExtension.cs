using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class AppointmentMapperExtension
{
    public static AppointmentReadDto AppointmentToReadDto(this Appointment appointment)
    {
        return new AppointmentReadDto()
        {
            Id = appointment.Id,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Status = appointment.Status,
            ClientId = appointment.ClientId,
            ServiceId = appointment.ServiceId
        };
    }

    public static Appointment UpdateDtoToAppointment(this Appointment appointment, AppointmentUpdateDto updateDto)
    {
        appointment.StartTime = updateDto.StartTime;
        appointment.EndTime = updateDto.EndTime;
        appointment.Status = updateDto.Status;
        appointment.ClientId = updateDto.ClientId;
        appointment.ServiceId = updateDto.ServiceId;
        appointment.Version += 1;
        appointment.UpdatedAt = DateTime.UtcNow;
        return appointment;
    }

    public static Appointment CreateDtoToAppointment(this AppointmentCreateDto createDto)
    {
        return new Appointment()
        {
            StartTime = createDto.StartTime,
            EndTime = createDto.EndTime,
            Status = AppointmentStatus.Reserved,
            ClientId = createDto.ClientId,
            ServiceId = createDto.ServiceId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Appointment DeleteDtoToAppointment(this Appointment appointment)
    {
        appointment.IsDeleted = true;
        appointment.DeletedAt = DateTime.UtcNow;
        appointment.Version += 1;
        appointment.UpdatedAt = DateTime.UtcNow;
        return appointment;
    }
}