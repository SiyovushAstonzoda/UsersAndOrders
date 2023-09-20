using Domain.Models.UserDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace UOApi.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Create User")]
    public async Task<string> CreateUser(AUUserDto user)
    {
        return await _userService.AddUser(user);
    }

    [HttpPut("Update User")]
    public async Task<string> UpdateUser(AUUserDto user)
    {
        return await _userService.UpdateUser(user);
    }

    [HttpDelete("Delete User")]
    public async Task<string> DeleteUser(int id)
    {
        return await _userService.DeleteUser(id);
    }

    [HttpGet("Get all Users")]
    public async Task<IEnumerable<GUserDto>> GetAllUsers()
    {
        return await _userService.GetAllUsers();
    }

    [HttpGet("Get User by Id")]
    public async Task<GUserDto> GetUserById(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpGet("Get all Users with list of Orders")]
    public async Task<IEnumerable<GUsersAndOrdersDto>> GetUsersAndOrders()
    {
        return await _userService.GetAllUsersWithOrders();
    }

    [HttpGet("Get top Users with a number of Orders")]
    public async Task<IEnumerable<GTopUsersDto>> GetTopUsers()
    {
        return await _userService.GetTopUsers();
    }
}
