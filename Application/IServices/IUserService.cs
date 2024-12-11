

using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<ResponseObject<DataResponseUser>> GetCurrentUser();
        Task<ResponseObject<DataResponseUser>> GetUserById();

    }
}
