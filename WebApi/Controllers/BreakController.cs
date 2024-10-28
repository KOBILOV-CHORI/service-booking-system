using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.BreakServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/breaks")]
public sealed class BreakController(IBreakService breakService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetBreaks([FromQuery] BreakFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<BreakReadDto>>>.Success(null,
            breakService.GetAllBreaks(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetBreakById(int id)
    {
        BreakReadDto? res = breakService.GetBreakById(id);
        return res != null
            ? Ok(ApiResponse<BreakReadDto?>.Success(null, res))
            : NotFound(ApiResponse<BreakReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateBreak([FromBody] BreakCreateDto breakCreateDto)
    {
        BreakCreateDto info = breakCreateDto;
        bool res = breakService.CreateBreak(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateBreak(BreakUpdateDto info)
    {
        bool res = breakService.UpdateBreak(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBreak(int id)
    {
        bool res = breakService.DeleteBreak(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}