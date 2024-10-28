namespace Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }

    public int OwnerId { get; set; }
    public User Owner { get; set; } // Используем User с Role = Owner

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public List<Review> Reviews { get; set; } = new List<Review>();
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
}