namespace Domain.Dtos;

public record CompanyUpdateDto : BaseCompanyDto
{
    public int Id { get; set; }
}

public record CompanyCreateDto : BaseCompanyDto {}

public record CompanyReadDto : BaseCompanyDto
{
    public int Id { get; set; }
}

public record BaseCompanyDto
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public int AddressId { get; set; }
}