using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.ServiceServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/services")]
public sealed class ServiceController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetServices([FromQuery] ServiceFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ServiceReadDto>>>.Success(null,
            serviceService.GetAllServices(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetServiceById(int id)
    {
        ServiceReadDto? res = serviceService.GetServiceById(id);
        return res != null
            ? Ok(ApiResponse<ServiceReadDto?>.Success(null, res))
            : NotFound(ApiResponse<ServiceReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateService([FromBody] ServiceCreateDto serviceCreateDto)
    {
        ServiceCreateDto info = serviceCreateDto;
        bool res = serviceService.CreateService(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateService(ServiceUpdateDto info)
    {
        bool res = serviceService.UpdateService(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteService(int id)
    {
        bool res = serviceService.DeleteService(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}