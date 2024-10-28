using Domain.Entities;

namespace Domain.Filters;

public record CountryFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}