using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Services.UserServices;

public interface IUserService
{
    PaginationResponse<IEnumerable<UserReadDto>> GetAllUsers(UserFilter filter);    
    UserReadDto? GetUserById(int id);
    bool CreateUser(UserCreateDto createdto);
    bool UpdateUser(UserUpdateDto updatedto);
    bool DeleteUser(int id);
}