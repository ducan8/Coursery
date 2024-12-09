using Application.Payloads.RequestModels.DataUser;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using Domain.Entities;
using System.Security.Claims;

namespace Application.IServices
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseUser>> Register(DataRequestUser_Register register);
        Task<ResponseObject<string>> ConfirmRegisterAccount(string confirmCode);
        Task<ResponseObject<DataResponseUser_Login>> Login(DataRequestUser_Login login);
        Task<ResponseObject<DataResponseUser>> ChangePassword(Guid userId, DataRequestUser_ChangePassword changePassword);
        Task<ResponseObject<string>> ForgotPassword(string email);
        Task<ResponseObject<string>> CreateNewPassword(DataRequestUser_CreateNewPassword request);
        Task<ResponseObject<string>> AddRolesToUser(Guid userId, List<string> roles);
        Task<ResponseObject<string>> DeleteRoles(Guid userId, List<string> roles);
        ClaimsPrincipal GetCurrentUser();
    }
}

        //Task<ResponseObject<DataResponseUser_Login>> GetJwtTokenAsync(User user);