using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.SpecialBreakServices;

public class SpecialBreakService(DataContext context) : ISpecialBreakService
{
    public PaginationResponse<IEnumerable<SpecialBreakReadDto>> GetAllSpecialBreaks(SpecialBreakFilter filter)
    {
        IQueryable<SpecialBreak> specialBreaks = context.SpecialBreaks;
        if (filter.WorkingHoursId != null)
            specialBreaks = specialBreaks.Where(x => x.WorkingHoursId == filter.WorkingHoursId);
        if (filter.StartTime != null)
            specialBreaks = specialBreaks.Where(x => x.StartTime >= filter.StartTime);
        if (filter.EndTime != null)
            specialBreaks = specialBreaks.Where(x => x.EndTime <= filter.EndTime);

        int totalRecords = specialBreaks.Count();
        var result = specialBreaks.Skip((filter.PageNumber - 1) * filter.PageSize)
                                  .Take(filter.PageSize)
                                  .Where(x => !x.IsDeleted)
                                  .Select(x => x.SpecialBreakToReadDto())
                                  .ToList();

        return PaginationResponse<IEnumerable<SpecialBreakReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public SpecialBreakReadDto? GetSpecialBreakById(int id)
    {
        return context.SpecialBreaks.Where(x => !x.IsDeleted && x.Id == id)
                                    .Select(x => x.SpecialBreakToReadDto())
                                    .FirstOrDefault();
    }

    public bool CreateSpecialBreak(SpecialBreakCreateDto createDto)
    {
        context.SpecialBreaks.Add(createDto.CreateDtoToSpecialBreak());
        context.SaveChanges();
        return true;
    }

    public bool UpdateSpecialBreak(SpecialBreakUpdateDto updateDto)
    {
        var existingSpecialBreak = context.SpecialBreaks.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingSpecialBreak == null) return false;

        existingSpecialBreak.UpdateDtoToSpecialBreak(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteSpecialBreak(int id)
    {
        var existingSpecialBreak = context.SpecialBreaks.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingSpecialBreak == null) return false;

        existingSpecialBreak.IsDeleted = true;
        existingSpecialBreak.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
