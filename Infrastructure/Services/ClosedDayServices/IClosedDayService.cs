using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.ClosedDayServices;

public interface IClosedDayService
{
    PaginationResponse<IEnumerable<ClosedDayReadDto>> GetAllClosedDays(ClosedDayFilter filter);    
    ClosedDayReadDto? GetClosedDayById(int id);
    bool CreateClosedDay(ClosedDayCreateDto createDto);
    bool UpdateClosedDay(ClosedDayUpdateDto updateDto);
    bool DeleteClosedDay(int id);
}