namespace Domain.Dtos;

public record CategoryUpdateDto : BaseCategoryDto
{
    public int Id { get; set; }
}

public record CategoryCreateDto : BaseCategoryDto {}

public record CategoryReadDto : BaseCategoryDto
{
    public int Id { get; set; }
}

public record BaseCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}