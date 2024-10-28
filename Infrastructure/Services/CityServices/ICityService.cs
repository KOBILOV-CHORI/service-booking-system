using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.CityServices;

public interface ICityService
{
    PaginationResponse<IEnumerable<CityReadDto>> GetAllCities(CityFilter filter);    
    CityReadDto? GetCityById(int id);
    bool CreateCity(CityCreateDto createDto);
    bool UpdateCity(CityUpdateDto updateDto);
    bool DeleteCity(int id);
}