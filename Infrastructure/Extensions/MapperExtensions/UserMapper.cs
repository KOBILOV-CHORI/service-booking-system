using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class UserMapperExtension
{
    public static UserReadDto UserToReadDto(this User user)
    {
        return new UserReadDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            AddressId = user.AddressId
        };
    }

    public static User UpdateDtoToUser(this User user, UserUpdateDto updateDto)
    {
        user.Id = updateDto.Id;
        user.FirstName = updateDto.FirstName;
        user.PhoneNumber = updateDto.PhoneNumber;
        user.Email = updateDto.Email;
        user.LastName = updateDto.LastName;
        user.UserName = updateDto.UserName;
        user.DateOfBirth = updateDto.DateOfBirth;
        user.AddressId = updateDto.AddressId;
        user.Role = updateDto.Role;
        user.Version += 1;
        user.UpdatedAt = DateTime.UtcNow;
        return user;
    }

    public static User CreateDtoToUser(this UserCreateDto createDto)
    {
        return new User()
        {
            FirstName = createDto.FirstName,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            LastName = createDto.LastName,
            UserName = createDto.UserName,
            DateOfBirth = createDto.DateOfBirth,
            AddressId = createDto.AddressId,
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static User DeleteDtoToUser(this User user)
    {
        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.Version += 1;
        user.UpdatedAt = DateTime.UtcNow;
        return user;
    }
}