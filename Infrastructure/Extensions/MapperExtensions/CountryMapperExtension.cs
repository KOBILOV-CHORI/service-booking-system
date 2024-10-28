using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class CountryMapperExtension
{
    public static CountryReadDto CountryToReadDto(this Country country)
    {
        return new CountryReadDto()
        {
            Id = country.Id,
            Name = country.Name,
            Code = country.Code
        };
    }

    public static Country UpdateDtoToCountry(this Country country, CountryUpdateDto updateDto)
    {
        country.Name = updateDto.Name;
        country.Code = updateDto.Code;
        country.Version += 1;
        country.UpdatedAt = DateTime.UtcNow;
        return country;
    }

    public static Country CreateDtoToCountry(this CountryCreateDto createDto)
    {
        return new Country()
        {
            Name = createDto.Name,
            Code = createDto.Code,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Country DeleteDtoToCountry(this Country country)
    {
        country.IsDeleted = true;
        country.DeletedAt = DateTime.UtcNow;
        country.Version += 1;
        country.UpdatedAt = DateTime.UtcNow;
        return country;
    }
}