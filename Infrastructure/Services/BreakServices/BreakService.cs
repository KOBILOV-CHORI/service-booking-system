using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.BreakServices;

public class BreakService(DataContext context) : IBreakService
{
    public PaginationResponse<IEnumerable<BreakReadDto>> GetAllBreaks(BreakFilter filter)
    {
        IQueryable<Break> breaks = context.Breaks;
        if (filter.WorkingHoursId != null)
            breaks = breaks.Where(x => x.WorkingHoursId == filter.WorkingHoursId);
        if (filter.StartTime != null)
            breaks = breaks.Where(x => x.StartTime >= filter.StartTime);
        if (filter.EndTime != null)
            breaks = breaks.Where(x => x.EndTime <= filter.EndTime);

        int totalRecords = breaks.Count();
        var result = breaks.Skip((filter.PageNumber - 1) * filter.PageSize)
                           .Take(filter.PageSize)
                           .Where(x => !x.IsDeleted)
                           .Select(x => x.BreakToReadDto())
                           .ToList();

        return PaginationResponse<IEnumerable<BreakReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public BreakReadDto? GetBreakById(int id)
    {
        return context.Breaks.Where(x => !x.IsDeleted && x.Id == id)
                             .Select(x => x.BreakToReadDto())
                             .FirstOrDefault();
    }

    public bool CreateBreak(BreakCreateDto createDto)
    {
        context.Breaks.Add(createDto.CreateDtoToBreak());
        context.SaveChanges();
        return true;
    }

    public bool UpdateBreak(BreakUpdateDto updateDto)
    {
        var existingBreak = context.Breaks.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingBreak == null) return false;

        existingBreak.UpdateDtoToBreak(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteBreak(int id)
    {
        var existingBreak = context.Breaks.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingBreak == null) return false;

        existingBreak.IsDeleted = true;
        existingBreak.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
