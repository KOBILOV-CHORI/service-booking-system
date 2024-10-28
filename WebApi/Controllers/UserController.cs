using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/users")]
public sealed class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers([FromQuery] UserFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<UserReadDto>>>.Success(null,
            userService.GetAllUsers(filter)));

    [HttpGet("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        UserReadDto? res = userService.GetUserById(id);
        return res != null
            ? Ok(ApiResponse<UserReadDto?>.Success(null, res))
            : NotFound(ApiResponse<UserReadDto?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDto userCreateDto)
    {
        UserCreateDto info = userCreateDto;
        bool res = userService.CreateUser(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateUser(UserUpdateDto info)
    {
        bool res = userService.UpdateUser(info);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        bool res = userService.DeleteUser(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}