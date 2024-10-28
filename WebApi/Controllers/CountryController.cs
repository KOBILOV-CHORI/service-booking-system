using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.CountryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/countries")]
public sealed class CountryController(ICountryService countryService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCountries([FromQuery] CountryFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<CountryReadDto>>>.Success(null,
            countryService.GetAllCountries(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetCountryById(int id)
    {
        CountryReadDto? res = countryService.GetCountryById(id);
        return res != null
            ? Ok(ApiResponse<CountryReadDto?>.Success(null, res))
            : NotFound(ApiResponse<CountryReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateCountry([FromBody] CountryCreateDto countryCreateDto)
    {
        CountryCreateDto info = countryCreateDto;
        bool res = countryService.CreateCountry(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCountry(CountryUpdateDto info)
    {
        bool res = countryService.UpdateCountry(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteCountry(int id)
    {
        bool res = countryService.DeleteCountry(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}