using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.CountryServices;

public interface ICountryService
{
    PaginationResponse<IEnumerable<CountryReadDto>> GetAllCountries(CountryFilter filter);    
    CountryReadDto? GetCountryById(int id);
    bool CreateCountry(CountryCreateDto createDto);
    bool UpdateCountry(CountryUpdateDto updateDto);
    bool DeleteCountry(int id);
}