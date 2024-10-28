using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.SpecialBreakServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/specialBreaks")]
public sealed class SpecialBreakController(ISpecialBreakService specialBreakService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetSpecialBreaks([FromQuery] SpecialBreakFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<SpecialBreakReadDto>>>.Success(null,
            specialBreakService.GetAllSpecialBreaks(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetSpecialBreakById(int id)
    {
        SpecialBreakReadDto? res = specialBreakService.GetSpecialBreakById(id);
        return res != null
            ? Ok(ApiResponse<SpecialBreakReadDto?>.Success(null, res))
            : NotFound(ApiResponse<SpecialBreakReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateSpecialBreak([FromBody] SpecialBreakCreateDto specialBreakCreateDto)
    {
        SpecialBreakCreateDto info = specialBreakCreateDto;
        bool res = specialBreakService.CreateSpecialBreak(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateSpecialBreak(SpecialBreakUpdateDto info)
    {
        bool res = specialBreakService.UpdateSpecialBreak(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteSpecialBreak(int id)
    {
        bool res = specialBreakService.DeleteSpecialBreak(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}