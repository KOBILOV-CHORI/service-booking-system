using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class AddressMapperExtension
{
    public static AddressReadDto AddressToReadDto(this Address address)
    {
        return new AddressReadDto()
        {
            Id = address.Id,
            Street = address.Street,
            District = address.District,
            CityId = address.CityId
        };
    }

    public static Address UpdateDtoToAddress(this Address address, AddressUpdateDto updateDto)
    {
        address.Street = updateDto.Street;
        address.District = updateDto.District;
        address.CityId = updateDto.CityId;
        address.Version += 1;
        address.UpdatedAt = DateTime.UtcNow;
        return address;
    }

    public static Address CreateDtoToAddress(this AddressCreateDto createDto)
    {
        return new Address()
        {
            Street = createDto.Street,
            District = createDto.District,
            CityId = createDto.CityId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Address DeleteDtoToAddress(this Address address)
    {
        address.IsDeleted = true;
        address.DeletedAt = DateTime.UtcNow;
        address.Version += 1;
        address.UpdatedAt = DateTime.UtcNow;
        return address;
    }
}