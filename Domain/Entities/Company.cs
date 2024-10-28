namespace Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public int AddressId { get; set; }
    public Address Address { get; set; }

    public List<User> Owners { get; set; } = new List<User>(); // Используем User с Role = Owner
}