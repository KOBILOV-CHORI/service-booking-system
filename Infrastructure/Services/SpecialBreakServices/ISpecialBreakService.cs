using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.SpecialBreakServices;

public interface ISpecialBreakService
{
    PaginationResponse<IEnumerable<SpecialBreakReadDto>> GetAllSpecialBreaks(SpecialBreakFilter filter);    
    SpecialBreakReadDto? GetSpecialBreakById(int id);
    bool CreateSpecialBreak(SpecialBreakCreateDto createDto);
    bool UpdateSpecialBreak(SpecialBreakUpdateDto updateDto);
    bool DeleteSpecialBreak(int id);
}