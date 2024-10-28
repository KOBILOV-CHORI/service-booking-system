using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.AddressServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/addresss")]
public sealed class AddressController(IAddressService addressService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAddresses([FromQuery] AddressFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<AddressReadDto>>>.Success(null,
            addressService.GetAllAddresses(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetAddressById(int id)
    {
        AddressReadDto? res = addressService.GetAddressById(id);
        return res != null
            ? Ok(ApiResponse<AddressReadDto?>.Success(null, res))
            : NotFound(ApiResponse<AddressReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateAddress([FromBody] AddressCreateDto addressCreateDto)
    {
        AddressCreateDto info = addressCreateDto;
        bool res = addressService.CreateAddress(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateAddress(AddressUpdateDto info)
    {
        bool res = addressService.UpdateAddress(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAddress(int id)
    {
        bool res = addressService.DeleteAddress(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}