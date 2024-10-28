using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.ClosedDayServices;

public class ClosedDayService(DataContext context) : IClosedDayService
{
    public PaginationResponse<IEnumerable<ClosedDayReadDto>> GetAllClosedDays(ClosedDayFilter filter)
    {
        IQueryable<ClosedDay> closedDays = context.ClosedDays;
        if (filter.OwnerId != null)
            closedDays = closedDays.Where(x => x.OwnerId == filter.OwnerId);
        if (filter.Date != null)
            closedDays = closedDays.Where(x => x.Date == filter.Date);

        int totalRecords = closedDays.Count();
        var result = closedDays.Skip((filter.PageNumber - 1) * filter.PageSize)
                               .Take(filter.PageSize)
                               .Where(x => !x.IsDeleted)
                               .Select(x => x.ClosedDayToReadDto())
                               .ToList();

        return PaginationResponse<IEnumerable<ClosedDayReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ClosedDayReadDto? GetClosedDayById(int id)
    {
        return context.ClosedDays.Where(x => !x.IsDeleted && x.Id == id)
                                 .Select(x => x.ClosedDayToReadDto())
                                 .FirstOrDefault();
    }

    public bool CreateClosedDay(ClosedDayCreateDto createDto)
    {
        context.ClosedDays.Add(createDto.CreateDtoToClosedDay());
        context.SaveChanges();
        return true;
    }

    public bool UpdateClosedDay(ClosedDayUpdateDto updateDto)
    {
        var existingClosedDay = context.ClosedDays.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingClosedDay == null) return false;

        existingClosedDay.UpdateDtoToClosedDay(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteClosedDay(int id)
    {
        var existingClosedDay = context.ClosedDays.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingClosedDay == null) return false;

        existingClosedDay.IsDeleted = true;
        existingClosedDay.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
