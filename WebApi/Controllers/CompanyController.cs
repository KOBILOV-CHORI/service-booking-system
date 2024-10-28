using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/companies")]
public sealed class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCompanies([FromQuery] CompanyFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<CompanyReadDto>>>.Success(null,
            companyService.GetAllCompanies(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetCompanyById(int id)
    {
        CompanyReadDto? res = companyService.GetCompanyById(id);
        return res != null
            ? Ok(ApiResponse<CompanyReadDto?>.Success(null, res))
            : NotFound(ApiResponse<CompanyReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyCreateDto companyCreateDto)
    {
        CompanyCreateDto info = companyCreateDto;
        bool res = companyService.CreateCompany(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCompany(CompanyUpdateDto info)
    {
        bool res = companyService.UpdateCompany(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteCompany(int id)
    {
        bool res = companyService.DeleteCompany(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}