using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.CompanyServices;

public class CompanyService(DataContext context) : ICompanyService
{
    public PaginationResponse<IEnumerable<CompanyReadDto>> GetAllCompanies(CompanyFilter filter)
    {
        IQueryable<Company> companies = context.Companies;
        if (!string.IsNullOrEmpty(filter.Name))
            companies = companies.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.Phone))
            companies = companies.Where(x => x.Phone.Contains(filter.Phone));
        if (filter.AddressId != null)
            companies = companies.Where(x => x.AddressId == filter.AddressId);

        int totalRecords = companies.Count();
        var result = companies.Skip((filter.PageNumber - 1) * filter.PageSize)
                              .Take(filter.PageSize)
                              .Where(x => !x.IsDeleted)
                              .Select(x => x.CompanyToReadDto())
                              .ToList();

        return PaginationResponse<IEnumerable<CompanyReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public CompanyReadDto? GetCompanyById(int id)
    {
        return context.Companies.Where(x => x.IsDeleted == false && x.Id == id)
                                .Select(x => x.CompanyToReadDto())
                                .FirstOrDefault();
    }

    public bool CreateCompany(CompanyCreateDto createDto)
    {
        context.Companies.Add(createDto.CreateDtoToCompany());
        context.SaveChanges();
        return true;
    }

    public bool UpdateCompany(CompanyUpdateDto updateDto)
    {
        var existingCompany = context.Companies.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingCompany == null) return false;

        existingCompany.UpdateDtoToCompany(updateDto);
        context.SaveChanges();
        return true;
    }

    public bool DeleteCompany(int id)
    {
        var existingCompany = context.Companies.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingCompany == null) return false;

        existingCompany.IsDeleted = true;
        existingCompany.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}
