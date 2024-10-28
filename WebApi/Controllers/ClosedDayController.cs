using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.ClosedDayServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/closed-days")]
public sealed class ClosedDayController(IClosedDayService closedDayService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetClosedDays([FromQuery] ClosedDayFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ClosedDayReadDto>>>.Success(null,
            closedDayService.GetAllClosedDays(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetClosedDayById(int id)
    {
        ClosedDayReadDto? res = closedDayService.GetClosedDayById(id);
        return res != null
            ? Ok(ApiResponse<ClosedDayReadDto?>.Success(null, res))
            : NotFound(ApiResponse<ClosedDayReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateClosedDay([FromBody] ClosedDayCreateDto closedDayCreateDto)
    {
        ClosedDayCreateDto info = closedDayCreateDto;
        bool res = closedDayService.CreateClosedDay(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateClosedDay(ClosedDayUpdateDto info)
    {
        bool res = closedDayService.UpdateClosedDay(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteClosedDay(int id)
    {
        bool res = closedDayService.DeleteClosedDay(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}