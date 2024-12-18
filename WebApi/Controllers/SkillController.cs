using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Service.SkillService;
using Infrastructure.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController(ISkillService skillService):ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Skill>>> GetAll()
    {
        var response = await skillService.GetAll();
        return response;
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<Response<Skill>> GetById(int id)
    {
        return skillService.GetById(id).Result;
    }

    [HttpPost("AddUser")]
    public async Task<Response<bool>> Create(Skill skill)
    {
        var response = await skillService.Create(skill);
        return response;
    }

    [HttpPut("UpdateUser")]
    public async Task<Response<bool>> Update(Skill skill)
    {
        var response = await skillService.Update(skill);
        return response;
    }

    [HttpDelete("DeleteUser")]
    public async Task<Response<bool>> Delete(int id)
    {
        var response = await skillService.Delete(id);
        return response;
    }
}