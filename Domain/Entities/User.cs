namespace Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

    public int AddressId { get; set; }
    public Address Address { get; set; }

    // Только для клиентов
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public List<Review> Reviews { get; set; } = new List<Review>();

    // Только для владельцев
    public List<Service> Services { get; set; } = new List<Service>();
    public List<WorkingHours> WorkingHours { get; set; } = new List<WorkingHours>();
    public List<ClosedDay> ClosedDays { get; set; } = new List<ClosedDay>();
}