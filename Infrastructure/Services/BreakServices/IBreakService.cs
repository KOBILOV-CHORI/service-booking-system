using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.BreakServices;

public interface IBreakService
{
    PaginationResponse<IEnumerable<BreakReadDto>> GetAllBreaks(BreakFilter filter);    
    BreakReadDto? GetBreakById(int id);
    bool CreateBreak(BreakCreateDto createDto);
    bool UpdateBreak(BreakUpdateDto updateDto);
    bool DeleteBreak(int id);
}