using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeSkills.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController(IGenericService<User> userService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<User>>> GetAll()
    {
        return await userService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<User>> GetById(int id)
    {
        return await userService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] User user)
    {
        return await userService.Add(user);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] User user)
    {
        return await userService.Update(user);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await userService.Delete(id);
    }
}