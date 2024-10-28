using Domain.Entities;

namespace Domain.Filters;

public record ServiceFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? OwnerId { get; set; }
    public int? CategoryId { get; set; }
}