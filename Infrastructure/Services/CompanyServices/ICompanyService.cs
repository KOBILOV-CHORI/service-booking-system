using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.CompanyServices;

public interface ICompanyService
{
    PaginationResponse<IEnumerable<CompanyReadDto>> GetAllCompanies(CompanyFilter filter);    
    CompanyReadDto? GetCompanyById(int id);
    bool CreateCompany(CompanyCreateDto createDto);
    bool UpdateCompany(CompanyUpdateDto updateDto);
    bool DeleteCompany(int id);
}