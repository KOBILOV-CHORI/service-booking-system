using Domain.Entities;

namespace Domain.Filters;

public record ReviewFilter : BaseFilter
{
    public int? ServiceId { get; set; }
    public int? ClientId { get; set; }
    public int? MinRating { get; set; }
    public int? MaxRating { get; set; }
    public string? Comment { get; set; }
}