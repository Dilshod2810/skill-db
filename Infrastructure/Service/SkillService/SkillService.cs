using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;
using Infrastructure.Service.SkillService;

namespace Infrastructure.Service.SkillService;

public class SkillService(DapperContext context) : ISkillService
{
    public async Task<Response<List<Skill>>> GetAll()
    {
        string sql = "select * from skills";
        var res = await context.Connection().QueryAsync<Skill>(sql);
        return new Response<List<Skill>>(res.ToList());
    }

    public async Task<Response<Skill>> GetById(int id)
    {
        string sql = "select * from skills where skillid=@Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<Skill>(sql, new { Id = id });
        return res == null
            ? new Response<Skill>(HttpStatusCode.NotFound, "Not found")
            : new Response<Skill>(HttpStatusCode.OK, "Skill is found");
    }

    public async Task<Response<bool>> Create(Skill skill)
    {
        string sql =
            "insert into skills(userid, title, description, createdat) values(@userid, @title, @description, @createdat);";
        var res = await context.Connection().ExecuteAsync(sql, skill);
        return res == null
            ? new Response<bool>(HttpStatusCode.Created, "Created successfully")
            : new Response<bool>(HttpStatusCode.BadRequest, "Skill cannot be added");
    }

    public async Task<Response<bool>> Update(Skill skill)
    {
        string sql =
            "update skills set userid=@UserId, title=@Title, Description=@Description, createdat=@CreatedAt where skillid=@Id;";
        var res = await context.Connection().ExecuteAsync(sql, skill);
        return res > 0
            ? new Response<bool>(HttpStatusCode.OK, "Updated")
            : new Response<bool>(HttpStatusCode.NotFound, "Not found");
    }

    public async Task<Response<bool>> Delete(int id)
    {
        string sql = "delete from skills where skillid=@Id;";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        return res > 0
            ? new Response<bool>(HttpStatusCode.OK, "Deleted")
            : new Response<bool>(HttpStatusCode.NotFound, "Not Found");
    }
}