using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class SpecialBreakMapperExtension
{
    public static SpecialBreakReadDto SpecialBreakToReadDto(this SpecialBreak specialBreakEntity)
    {
        return new SpecialBreakReadDto
        {
            Id = specialBreakEntity.Id,
            WorkingHoursId = specialBreakEntity.WorkingHoursId,
            StartTime = specialBreakEntity.StartTime,
            EndTime = specialBreakEntity.EndTime
        };
    }

    public static SpecialBreak CreateDtoToSpecialBreak(this SpecialBreakCreateDto createDto)
    {
        return new SpecialBreak
        {
            WorkingHoursId = createDto.WorkingHoursId,
            StartTime = createDto.StartTime,
            EndTime = createDto.EndTime,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static SpecialBreak UpdateDtoToSpecialBreak(this SpecialBreak specialBreakEntity, SpecialBreakUpdateDto updateDto)
    {
        specialBreakEntity.WorkingHoursId = updateDto.WorkingHoursId;
        specialBreakEntity.StartTime = updateDto.StartTime;
        specialBreakEntity.EndTime = updateDto.EndTime;
        specialBreakEntity.UpdatedAt = DateTime.UtcNow;
        specialBreakEntity.Version += 1;
        return specialBreakEntity;
    }

    public static SpecialBreak DeleteDtoToSpecialBreak(this SpecialBreak specialBreakEntity)
    {
        specialBreakEntity.IsDeleted = true;
        specialBreakEntity.DeletedAt = DateTime.UtcNow;
        specialBreakEntity.UpdatedAt = DateTime.UtcNow;
        specialBreakEntity.Version += 1;
        return specialBreakEntity;
    }
}
