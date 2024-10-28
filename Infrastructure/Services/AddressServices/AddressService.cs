using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.AddressServices;

public class AddressService(DataContext context) : IAddressService
{
    public PaginationResponse<IEnumerable<AddressReadDto>> GetAllAddresses(AddressFilter filter)
    {
        IQueryable<Address> addresses = context.Addresses;

        if (!string.IsNullOrEmpty(filter.Street))
            addresses = addresses.Where(x => x.Street.ToLower().Contains(filter.Street.ToLower()));
        if (!string.IsNullOrEmpty(filter.District))
            addresses = addresses.Where(x => x.District.ToLower().Contains(filter.District.ToLower()));
        if (filter.CityId != null)
            addresses = addresses.Where(x => x.CityId == filter.CityId);

        int totalRecords = addresses.Count();
        var result = addresses.Skip((filter.PageNumber - 1) * filter.PageSize)
                              .Take(filter.PageSize)
                              .Where(x => !x.IsDeleted)
                              .Select(x => x.AddressToReadDto())
                              .ToList();

        return PaginationResponse<IEnumerable<AddressReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public AddressReadDto? GetAddressById(int id)
    {
        return context.Addresses.Where(x => !x.IsDeleted && x.Id == id)
                                .Select(x => x.AddressToReadDto())
                                .FirstOrDefault();
    }

    public bool CreateAddress(AddressCreateDto createDto)
    {
        context.Addresses.Add(createDto.CreateDtoToAddress());
        context.SaveChanges();
        return true;
    }

    public bool UpdateAddress(AddressUpdateDto updateDto)
    {
        var existingAddress = context.Addresses.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingAddress == null) return false;

        existingAddress.UpdateDtoToAddress(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteAddress(int id)
    {
        var existingAddress = context.Addresses.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingAddress == null) return false;

        existingAddress.IsDeleted = true;
        existingAddress.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
