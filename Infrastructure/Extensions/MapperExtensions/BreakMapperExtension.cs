using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class BreakMapperExtension
{
    public static BreakReadDto BreakToReadDto(this Break breakEntity)
    {
        return new BreakReadDto
        {
            Id = breakEntity.Id,
            WorkingHoursId = breakEntity.WorkingHoursId,
            StartTime = breakEntity.StartTime,
            EndTime = breakEntity.EndTime
        };
    }

    public static Break CreateDtoToBreak(this BreakCreateDto createDto)
    {
        return new Break
        {
            WorkingHoursId = createDto.WorkingHoursId,
            StartTime = createDto.StartTime,
            EndTime = createDto.EndTime,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Break UpdateDtoToBreak(this Break breakEntity, BreakUpdateDto updateDto)
    {
        breakEntity.WorkingHoursId = updateDto.WorkingHoursId;
        breakEntity.StartTime = updateDto.StartTime;
        breakEntity.EndTime = updateDto.EndTime;
        breakEntity.UpdatedAt = DateTime.UtcNow;
        breakEntity.Version += 1;
        return breakEntity;
    }

    public static Break DeleteDtoToBreak(this Break breakEntity)
    {
        breakEntity.IsDeleted = true;
        breakEntity.DeletedAt = DateTime.UtcNow;
        breakEntity.UpdatedAt = DateTime.UtcNow;
        breakEntity.Version += 1;
        return breakEntity;
    }
}