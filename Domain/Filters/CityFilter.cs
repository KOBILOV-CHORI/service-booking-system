using Domain.Entities;

namespace Domain.Filters;

public record CityFilter : BaseFilter
{
    public string? Name { get; set; }
    public int? CountryId { get; set; }
}