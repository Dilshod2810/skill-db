using System.Net;
using Dapper;
using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.DataContext;
using Infrastructure.Service.RequestService;

namespace Infrastructure.Service.RequestService;

public class RequestService(DapperContext context) : IRequestService
{
    public async Task<Response<List<Request>>> GetAll()
    {
        string sql = "select * from requests";
        var res = await context.Connection().QueryAsync<Request>(sql);
        return new Response<List<Request>>(res.ToList());
    }

    public async Task<Response<Request>> GetById(int id)
    {
        string sql = "select * from requests where requestid=@Id";
        var res = await context.Connection().QuerySingleOrDefaultAsync<Request>(sql, new { Id = id });
        return res == null
            ? new Response<Request>(HttpStatusCode.NotFound, "Not found")
            : new Response<Request>(HttpStatusCode.OK, "Request is found");
    }

    public async Task<Response<bool>> Create(Request request)
    {
        string sql =
            "insert into requests(fromuserid, touserid, requestedskillid, offeredskillid, status, createdat, updatedat) values(@fromuserid, @touserid, @requestedskillid, @offeredskillid, @status, @createdat, @updatedat);";
        var res = await context.Connection().ExecuteAsync(sql, request);
        return res == null
            ? new Response<bool>(HttpStatusCode.Created, "Created successfully")
            : new Response<bool>(HttpStatusCode.BadRequest, "Request cannot be added");
    }

    public async Task<Response<bool>> Update(Request request)
    {
        string sql =
            "update requests set fromuserid=@fromuserid, touserid=@touserid, requestedskillid=@requestedskillid, offeredskillid=@offeredskillid, status=@status, createdat=@createdat, updatedat=@updatedat where requestid=@Id;";
        var res = await context.Connection().ExecuteAsync(sql, request);
        return res > 0
            ? new Response<bool>(HttpStatusCode.OK, "Updated")
            : new Response<bool>(HttpStatusCode.NotFound, "Not found");
    }


}