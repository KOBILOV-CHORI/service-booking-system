namespace Domain.Dtos;

public record ReviewUpdateDto : BaseReviewDto
{
    public int Id { get; set; }
}

public record ReviewCreateDto : BaseReviewDto {}

public record ReviewReadDto : BaseReviewDto
{
    public int Id { get; set; }
}

public record BaseReviewDto
{
    public string Comment { get; set; }
    public int Rating { get; set; }

    public int ServiceId { get; set; }
    public int ClientId { get; set; }
}