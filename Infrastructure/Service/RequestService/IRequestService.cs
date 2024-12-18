using Domain.Models;
using Infrastructure.ApiResponse;

namespace Infrastructure.Service.RequestService;

public interface IRequestService
{
    Task<Response<List<Request>>> GetAll();
    Task<Response<Request>> GetById(int id);
    Task<Response<bool>> Create(Request request);
    Task<Response<bool>> Update(Request request);
}