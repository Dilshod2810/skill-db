using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.SkillService;

public interface ISkillService
{
    Task<Response<List<Skill>>> GetAll();
    Task<Response<Skill>> GetById(int id);
    Task<Response<bool>> Create(Skill skill);
    Task<Response<bool>> Update(Skill skill);
    Task<Response<bool>> Delete(int id);
}