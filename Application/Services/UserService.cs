using Application.IServices;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public ClaimsPrincipal GetCurrentUser()
        {
            return _contextAccessor.HttpContext.User;

        }

        public Task<ResponseObject<DataResponseUser>> GetUserById()
        {
            throw new NotImplementedException();
        }
    }
}
