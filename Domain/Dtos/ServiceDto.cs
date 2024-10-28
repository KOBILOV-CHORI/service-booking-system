namespace Domain.Dtos;

public record ServiceUpdateDto : BaseServiceDto
{
    public int Id { get; set; }
}

public record ServiceCreateDto : BaseServiceDto {}

public record ServiceReadDto : BaseServiceDto
{
    public int Id { get; set; }
}

public record BaseServiceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }

    public int OwnerId { get; set; }
    public int CategoryId { get; set; }
}