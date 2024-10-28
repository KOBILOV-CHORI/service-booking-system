namespace Domain.Entities;

public class Review : BaseEntity
{
    public string Comment { get; set; }
    public int Rating { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public int ClientId { get; set; }
    public User Client { get; set; } // Используем User с Role = Client
}