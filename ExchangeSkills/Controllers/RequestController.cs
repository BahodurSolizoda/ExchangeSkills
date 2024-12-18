using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeSkills.Controllers;


[ApiController]
[Route("[controller]")]

public class RequestController(IGenericService<Request> requestService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Request>>> GetAll()
    {
        return await requestService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Request>> GetById(int id)
    {
        return await requestService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Request request)
    {
        return await requestService.Add(request);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Request request)
    {
        return await requestService.Update(request);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await requestService.Delete(id);
    }
}