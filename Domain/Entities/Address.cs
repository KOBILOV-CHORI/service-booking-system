namespace Domain.Entities;

public class Address : BaseEntity
{
    public string Street { get; set; }
    public string District { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }

    public Company Company { get; set; } // Связь один к одному с Company
}