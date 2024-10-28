using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.WorkingHoursServices;

public interface IWorkingHoursService
{
    PaginationResponse<IEnumerable<WorkingHoursReadDto>> GetAllWorkingHours(WorkingHoursFilter filter);
    WorkingHoursReadDto? GetWorkingHoursById(int id);
    bool CreateWorkingHours(WorkingHoursCreateDto createDto);
    bool UpdateWorkingHours(WorkingHoursUpdateDto updateDto);
    bool DeleteWorkingHours(int id);
}