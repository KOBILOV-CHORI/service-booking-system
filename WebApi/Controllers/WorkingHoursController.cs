using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.WorkingHoursServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/workingHours")]
public sealed class WorkingHoursController(IWorkingHoursService workingHoursService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetWorkingHours([FromQuery] WorkingHoursFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<WorkingHoursReadDto>>>.Success(null,
            workingHoursService.GetAllWorkingHours(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetWorkingHoursById(int id)
    {
        WorkingHoursReadDto? res = workingHoursService.GetWorkingHoursById(id);
        return res != null
            ? Ok(ApiResponse<WorkingHoursReadDto?>.Success(null, res))
            : NotFound(ApiResponse<WorkingHoursReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateWorkingHours([FromBody] WorkingHoursCreateDto workingHoursCreateDto)
    {
        WorkingHoursCreateDto info = workingHoursCreateDto;
        bool res = workingHoursService.CreateWorkingHours(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateWorkingHours(WorkingHoursUpdateDto info)
    {
        bool res = workingHoursService.UpdateWorkingHours(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteWorkingHours(int id)
    {
        bool res = workingHoursService.DeleteWorkingHours(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}