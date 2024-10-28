using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class CompanyMapperExtension
{
    public static CompanyReadDto CompanyToReadDto(this Company company)
    {
        return new CompanyReadDto()
        {
            Id = company.Id,
            Name = company.Name,
            Phone = company.Phone,
            AddressId = company.AddressId
        };
    }

    public static Company UpdateDtoToCompany(this Company company, CompanyUpdateDto updateDto)
    {
        company.Name = updateDto.Name;
        company.Phone = updateDto.Phone;
        company.AddressId = updateDto.AddressId;
        company.Version += 1;
        company.UpdatedAt = DateTime.UtcNow;
        return company;
    }

    public static Company CreateDtoToCompany(this CompanyCreateDto createDto)
    {
        return new Company()
        {
            Name = createDto.Name,
            Phone = createDto.Phone,
            AddressId = createDto.AddressId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Company DeleteDtoToCompany(this Company company)
    {
        company.IsDeleted = true;
        company.DeletedAt = DateTime.UtcNow;
        company.Version += 1;
        company.UpdatedAt = DateTime.UtcNow;
        return company;
    }
}