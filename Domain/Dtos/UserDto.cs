using Domain.Entities;

namespace Domain.Dtos;

public record UserUpdateDto : BaseUserDto
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
}

public record UserCreateDto : BaseUserDto {}

public record UserReadDto : BaseUserDto
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
}

public record BaseUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int AddressId { get; set; }
}