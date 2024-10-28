using Domain.Entities;

namespace Domain.Filters;

public record AddressFilter : BaseFilter
{
    public string? Street { get; set; }
    public string? District { get; set; }
    public int? CityId { get; set; }
}