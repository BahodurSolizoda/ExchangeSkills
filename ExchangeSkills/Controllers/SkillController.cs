using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeSkills.Controllers;

[ApiController]
[Route("[controller]")]

public class SkillController(IGenericService<Skill> skillService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Skill>>> GetAll()
    {
        return await skillService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Skill>> GetById(int id)
    {
        return await skillService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Skill skill)
    {
        return await skillService.Add(skill);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Skill skill)
    {
        return await skillService.Update(skill);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await skillService.Delete(id);
    }
}