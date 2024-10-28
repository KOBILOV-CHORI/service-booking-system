using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.CityServices;

public class CityService(DataContext context) : ICityService
{
    public PaginationResponse<IEnumerable<CityReadDto>> GetAllCities(CityFilter filter)
    {
        IQueryable<City> cities = context.Cities;
        if (!string.IsNullOrEmpty(filter.Name))
            cities = cities.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (filter.CountryId != null)
            cities = cities.Where(x => x.CountryId == filter.CountryId);

        int totalRecords = cities.Count();
        var result = cities.Skip((filter.PageNumber - 1) * filter.PageSize)
                           .Take(filter.PageSize)
                           .Where(x => !x.IsDeleted)
                           .Select(x => x.CityToReadDto())
                           .ToList();

        return PaginationResponse<IEnumerable<CityReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public CityReadDto? GetCityById(int id)
    {
        return context.Cities.Where(x => x.IsDeleted == false && x.Id == id)
                             .Select(x => x.CityToReadDto())
                             .FirstOrDefault();
    }

    public bool CreateCity(CityCreateDto createDto)
    {
        context.Cities.Add(createDto.CreateDtoToCity());
        context.SaveChanges();
        return true;
    }

    public bool UpdateCity(CityUpdateDto updateDto)
    {
        var existingCity = context.Cities.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingCity == null) return false;

        existingCity.UpdateDtoToCity(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteCity(int id)
    {
        var existingCity = context.Cities.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingCity == null) return false;

        existingCity.IsDeleted = true;
        existingCity.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
