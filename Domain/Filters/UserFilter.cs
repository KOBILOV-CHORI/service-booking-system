using Domain.Entities;

namespace Domain.Filters;

public record UserFilter : BaseFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserRole? Role { get; set; } = UserRole.User;

    public int? AddressId { get; set; }
}