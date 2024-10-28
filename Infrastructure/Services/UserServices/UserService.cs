using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;

namespace Infrastructure.Services.UserServices;

public class UserService(DataContext context) : IUserService
{
    public PaginationResponse<IEnumerable<UserReadDto>> GetAllUsers(UserFilter filter)
    {
        IQueryable<User> users = context.Users;
        if (filter.FirstName != null)
            users = users.Where(x => x.FirstName.ToLower()
                .Contains(filter.FirstName.ToLower()));
        if (filter.LastName != null)
            users = users.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
        if (filter.UserName != null)
            users = users.Where(x => x.UserName.ToLower().Contains(filter.UserName.ToLower()));
        if (filter.Email != null)
            users = users.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        if (filter.PhoneNumber != null)
            users = users.Where(x => x.PhoneNumber.ToLower().Contains(filter.PhoneNumber.ToLower()));
        if (filter.DateOfBirth != DateTime.MinValue)
            users = users.Where(x => x.DateOfBirth == filter.DateOfBirth);
        if (filter.Role != UserRole.User)
            users = users.Where(x => x.Role == filter.Role);
        if (filter.AddressId != 0)
            users = users.Where(x => x.AddressId == filter.AddressId);
        IQueryable<UserReadDto> result = users.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Where(x => !x.IsDeleted).Select(x => x.UserToReadDto());

        int totalRecords = users.Count();

        return PaginationResponse<IEnumerable<UserReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords,
            result);
    }

    public UserReadDto? GetUserById(int id)
    {
        UserReadDto? res = context.Users.Where(x =>
                x.IsDeleted == false && x.Id == id).Select(x => x.UserToReadDto())
            .FirstOrDefault();
        return res ?? null;
    }

    public bool CreateUser(UserCreateDto createdto)
    {
        bool existingUser = context.Users.Any(x =>
            x.Email.ToLower() == createdto.Email.ToLower() || x.PhoneNumber == createdto.PhoneNumber || x.UserName.ToLower() == createdto.UserName.ToLower() || x.IsDeleted == false);
        if (existingUser) return false;

        context.Users.Add(createdto.CreateDtoToUser());
        context.SaveChanges();
        return true;
    }

    public bool UpdateUser(UserUpdateDto updatedto)
    {
        User? existingUser =
            context.Users.FirstOrDefault(x => x.IsDeleted == false && x.Id == updatedto.Id);
        if (existingUser is null) return false;
        context.Users.Update(existingUser.UpdateDtoToUser(updatedto));
        context.SaveChanges();
        return true;
    }

    public bool DeleteUser(int id)
    {
        User? existingUser = context.Users.FirstOrDefault(x => x.Id == id);
        if (existingUser is null) return false;
        existingUser.DeleteDtoToUser();
        context.SaveChanges();
        return true;
    }
}