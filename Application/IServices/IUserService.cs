using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using System.Security.Claims;

namespace Application.IServices
{
    public interface IUserService
    {
        ClaimsPrincipal GetCurrentUser();
        Task<ResponseObject<DataResponseUser>> GetUserById();

    }
}
