using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.CityServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/cities")]
public sealed class CityController(ICityService cityService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCities([FromQuery] CityFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<CityReadDto>>>.Success(null,
            cityService.GetAllCities(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetCityById(int id)
    {
        CityReadDto? res = cityService.GetCityById(id);
        return res != null
            ? Ok(ApiResponse<CityReadDto?>.Success(null, res))
            : NotFound(ApiResponse<CityReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateCity([FromBody] CityCreateDto cityCreateDto)
    {
        CityCreateDto info = cityCreateDto;
        bool res = cityService.CreateCity(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCity(CityUpdateDto info)
    {
        bool res = cityService.UpdateCity(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteCity(int id)
    {
        bool res = cityService.DeleteCity(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}