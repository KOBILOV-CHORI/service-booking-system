namespace Domain.Dtos;

public record AddressUpdateDto : BaseAddressDto
{
    public int Id { get; set; }
}

public record AddressCreateDto : BaseAddressDto {}

public record AddressReadDto : BaseAddressDto
{
    public int Id { get; set; }
}

public record BaseAddressDto
{
    public string Street { get; set; }
    public string District { get; set; }

    public int CityId { get; set; }
}