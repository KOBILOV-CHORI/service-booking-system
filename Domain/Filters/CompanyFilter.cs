using Domain.Entities;

namespace Domain.Filters;

public record CompanyFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? AddressId { get; set; }
}