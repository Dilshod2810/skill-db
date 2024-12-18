using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;

namespace Infrastructure.Service.UserService;

public class UserService(DapperContext context):IUserService
{
    public async Task<Response<List<User>>> GetAll()
    {
        string sql = "select * from users";
        var res = await context.Connection().QueryAsync<User>(sql);
        return new Response<List<User>>(res.ToList());
    }

    public async Task<Response<User>> GetById(int id)
    {
        string sql = "select * from users where userid=@Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        return res == null
            ? new Response<User>(HttpStatusCode.NotFound, "Not found")
            : new Response<User>(HttpStatusCode.OK, "User is found");
    }

    public async Task<Response<bool>> Create(User user)
    {
        string sql =
            "insert into users(fullname, email, phone, city, createdat) values(@fullname, @email, @phone, @city, @createdat);";
        var res = await context.Connection().ExecuteAsync(sql, user);
        return res == null
            ? new Response<bool>(HttpStatusCode.Created, "Created successfully")
            : new Response<bool>(HttpStatusCode.BadRequest, "User cannot be added");
    }

    public async Task<Response<bool>> Update(User user)
    {
        string sql =
            "update users set fullname=@FullName, email=@Email, phone=@Phone, city=@City, createdat=@CreatedAt where userid=@Id;";
        var res = await context.Connection().ExecuteAsync(sql, user);
        return res > 0
            ? new Response<bool>(HttpStatusCode.OK, "Updated")
            : new Response<bool>(HttpStatusCode.NotFound, "Not found");
    }

    public async Task<Response<bool>> Delete(int id)
    {
        string sql = "delete from users where userid=@Id;";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        return res > 0
            ? new Response<bool>(HttpStatusCode.OK, "Deleted")
            : new Response<bool>(HttpStatusCode.NotFound, "Not Found");
    }
}