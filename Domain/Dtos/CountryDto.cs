namespace Domain.Dtos;

public record CountryUpdateDto : BaseCountryDto
{
    public int Id { get; set; }
}

public record CountryCreateDto : BaseCountryDto {}

public record CountryReadDto : BaseCountryDto
{
    public int Id { get; set; }
}

public record BaseCountryDto
{
    public string Name { get; set; }
    public string Code { get; set; }
}