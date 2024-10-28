using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.AddressServices;

public interface IAddressService
{
    PaginationResponse<IEnumerable<AddressReadDto>> GetAllAddresses(AddressFilter filter);
    AddressReadDto? GetAddressById(int id);
    bool CreateAddress(AddressCreateDto createDto);
    bool UpdateAddress(AddressUpdateDto updateDto);
    bool DeleteAddress(int id);
}