namespace Domain.Filters;

public record CategoryFilter : BaseFilter
{
    public string? Name { get; set; }
}