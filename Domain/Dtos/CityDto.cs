namespace Domain.Dtos;

public record CityUpdateDto : BaseCityDto
{
    public int Id { get; set; }
}

public record CityCreateDto : BaseCityDto {}

public record CityReadDto : BaseCityDto
{
    public int Id { get; set; }
}

public record BaseCityDto
{
    public string Name { get; set; }
    public int CountryId { get; set; }
}