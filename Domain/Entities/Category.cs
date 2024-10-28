namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Service> Services { get; set; } = new List<Service>();
}