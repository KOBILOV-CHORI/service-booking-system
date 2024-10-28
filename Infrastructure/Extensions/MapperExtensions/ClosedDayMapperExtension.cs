using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ClosedDayMapperExtension
{
    public static ClosedDayReadDto ClosedDayToReadDto(this ClosedDay closedDay)
    {
        return new ClosedDayReadDto()
        {
            Id = closedDay.Id,
            OwnerId = closedDay.OwnerId,
            Date = closedDay.Date,
            Reason = closedDay.Reason
        };
    }

    public static ClosedDay UpdateDtoToClosedDay(this ClosedDay closedDay, ClosedDayUpdateDto updateDto)
    {
        closedDay.Date = updateDto.Date;
        closedDay.Reason = updateDto.Reason;
        closedDay.OwnerId = updateDto.OwnerId;
        closedDay.Version += 1;
        closedDay.UpdatedAt = DateTime.UtcNow;
        return closedDay;
    }

    public static ClosedDay CreateDtoToClosedDay(this ClosedDayCreateDto createDto)
    {
        return new ClosedDay()
        {
            Date = createDto.Date,
            Reason = createDto.Reason,
            OwnerId = createDto.OwnerId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static ClosedDay DeleteDtoToClosedDay(this ClosedDay closedDay)
    {
        closedDay.IsDeleted = true;
        closedDay.DeletedAt = DateTime.UtcNow;
        closedDay.Version += 1;
        closedDay.UpdatedAt = DateTime.UtcNow;
        return closedDay;
    }
}