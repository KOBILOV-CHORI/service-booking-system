using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.WorkingHoursServices;

public class WorkingHoursService(DataContext context) : IWorkingHoursService
{
    public PaginationResponse<IEnumerable<WorkingHoursReadDto>> GetAllWorkingHours(WorkingHoursFilter filter)
    {
        IQueryable<WorkingHours> workingHours = context.WorkingHours;
        if (filter.OwnerId != null)
            workingHours = workingHours.Where(x => x.OwnerId == filter.OwnerId);
        if (filter.Day != null)
            workingHours = workingHours.Where(x => x.Day == filter.Day);
        if (filter.StartTime != null)
            workingHours = workingHours.Where(x => x.StartTime >= filter.StartTime);
        if (filter.EndTime != null)
            workingHours = workingHours.Where(x => x.EndTime <= filter.EndTime);

        int totalRecords = workingHours.Count();
        var result = workingHours.Skip((filter.PageNumber - 1) * filter.PageSize)
                                 .Take(filter.PageSize)
                                 .Where(x => !x.IsDeleted)
                                 .Select(x => x.WorkingHoursToReadDto())
                                 .ToList();

        return PaginationResponse<IEnumerable<WorkingHoursReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public WorkingHoursReadDto? GetWorkingHoursById(int id)
    {
        return context.WorkingHours.Where(x => !x.IsDeleted && x.Id == id)
                                   .Select(x => x.WorkingHoursToReadDto())
                                   .FirstOrDefault();
    }

    public bool CreateWorkingHours(WorkingHoursCreateDto createDto)
    {
        context.WorkingHours.Add(createDto.CreateDtoToWorkingHours());
        context.SaveChanges();
        return true;
    }

    public bool UpdateWorkingHours(WorkingHoursUpdateDto updateDto)
    {
        var existingWorkingHours = context.WorkingHours.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingWorkingHours == null) return false;

        existingWorkingHours.UpdateDtoToWorkingHours(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteWorkingHours(int id)
    {
        var existingWorkingHours = context.WorkingHours.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingWorkingHours == null) return false;

        existingWorkingHours.IsDeleted = true;
        existingWorkingHours.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
