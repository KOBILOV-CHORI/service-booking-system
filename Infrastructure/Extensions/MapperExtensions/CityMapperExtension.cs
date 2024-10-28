using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class CityMapperExtension
{
    public static CityReadDto CityToReadDto(this City city)
    {
        return new CityReadDto()
        {
            Id = city.Id,
            Name = city.Name,
            CountryId = city.CountryId
        };
    }

    public static City UpdateDtoToCity(this City city, CityUpdateDto updateDto)
    {
        city.Name = updateDto.Name;
        city.CountryId = updateDto.CountryId;
        city.Version += 1;
        city.UpdatedAt = DateTime.UtcNow;
        return city;
    }

    public static City CreateDtoToCity(this CityCreateDto createDto)
    {
        return new City()
        {
            Name = createDto.Name,
            CountryId = createDto.CountryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static City DeleteDtoToCity(this City city)
    {
        city.IsDeleted = true;
        city.DeletedAt = DateTime.UtcNow;
        city.Version += 1;
        city.UpdatedAt = DateTime.UtcNow;
        return city;
    }
}