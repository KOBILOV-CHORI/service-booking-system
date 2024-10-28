using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.CountryServices;

public class CountryService(DataContext context) : ICountryService
{
    public PaginationResponse<IEnumerable<CountryReadDto>> GetAllCountries(CountryFilter filter)
    {
        IQueryable<Country> countries = context.Countries;
        if (!string.IsNullOrEmpty(filter.Name))
            countries = countries.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Code))
            countries = countries.Where(x => x.Code.ToLower().Contains(filter.Code.ToLower()));

        int totalRecords = countries.Count();
        var result = countries.Skip((filter.PageNumber - 1) * filter.PageSize)
                              .Take(filter.PageSize)
                              .Where(x => !x.IsDeleted)
                              .Select(x => x.CountryToReadDto())
                              .ToList();

        return PaginationResponse<IEnumerable<CountryReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public CountryReadDto? GetCountryById(int id)
    {
        return context.Countries.Where(x => x.IsDeleted == false && x.Id == id)
                                .Select(x => x.CountryToReadDto())
                                .FirstOrDefault();
    }

    public bool CreateCountry(CountryCreateDto createDto)
    {
        context.Countries.Add(createDto.CreateDtoToCountry());
        context.SaveChanges();
        return true;
    }

    public bool UpdateCountry(CountryUpdateDto updateDto)
    {
        var existingCountry = context.Countries.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingCountry == null) return false;

        existingCountry.UpdateDtoToCountry(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteCountry(int id)
    {
        var existingCountry = context.Countries.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingCountry == null) return false;

        existingCountry.IsDeleted = true;
        existingCountry.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
