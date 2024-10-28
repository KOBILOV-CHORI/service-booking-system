using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class WorkingHoursMapperExtension
{
    public static WorkingHoursReadDto WorkingHoursToReadDto(this WorkingHours workingHours)
    {
        return new WorkingHoursReadDto()
        {
            Id = workingHours.Id,
            OwnerId = workingHours.OwnerId,
            Day = workingHours.Day,
            StartTime = workingHours.StartTime,
            EndTime = workingHours.EndTime
        };
    }

    public static WorkingHours UpdateDtoToWorkingHours(this WorkingHours workingHours, WorkingHoursUpdateDto updateDto)
    {
        workingHours.Day = updateDto.Day;
        workingHours.StartTime = updateDto.StartTime;
        workingHours.EndTime = updateDto.EndTime;
        workingHours.OwnerId = updateDto.OwnerId;
        workingHours.Version += 1;
        workingHours.UpdatedAt = DateTime.UtcNow;
        return workingHours;
    }

    public static WorkingHours CreateDtoToWorkingHours(this WorkingHoursCreateDto createDto)
    {
        return new WorkingHours()
        {
            Day = createDto.Day,
            StartTime = createDto.StartTime,
            EndTime = createDto.EndTime,
            OwnerId = createDto.OwnerId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static WorkingHours DeleteDtoToWorkingHours(this WorkingHours workingHours)
    {
        workingHours.IsDeleted = true;
        workingHours.DeletedAt = DateTime.UtcNow;
        workingHours.Version += 1;
        workingHours.UpdatedAt = DateTime.UtcNow;
        return workingHours;
    }
}