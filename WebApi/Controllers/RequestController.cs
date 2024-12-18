using Domain.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Service.RequestService;
using Infrastructure.Service.SkillService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestController(IRequestService requestService):ControllerBase
{
    [HttpGet]
    public async Task<Response<List<Request>>> GetAll()
    {
        var response = await requestService.GetAll();
        return response;
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<Response<Request>> GetById(int id)
    {
        return requestService.GetById(id).Result;
    }

    [HttpPost("AddUser")]
    public async Task<Response<bool>> Create(Request request)
    {
        var response = await requestService.Create(request);
        return response;
    }

    [HttpPut("UpdateUser")]
    public async Task<Response<bool>> Update(Request request)
    {
        var response = await requestService.Update(request);
        return response;
    }


}