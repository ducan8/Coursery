using Application.IServices;
using Application.Payloads.RequestModels.DataUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DataRequestUser_Register requestRegister)
        {
            var result = await _authService.Register(requestRegister);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromForm] string confirmCode)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    var fieldName = error.Key;
                    var errorMessages = string.Join("; ", error.Value.Errors.Select(e => e.ErrorMessage));
                    _logger.LogError($"Field: {fieldName}, Errors: {errorMessages}");
                }
            }
            var result = await _authService.ConfirmRegisterAccount(confirmCode);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] DataRequestUser_Login login)
        {
            var result = await _authService.Login(login);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromForm] DataRequestUser_ChangePassword changePassword)
        {
            await Console.Out.WriteLineAsync("ahihi");
            if (HttpContext.User.FindFirst("Id") != null)
            {
                Guid id = Guid.Parse(HttpContext.User.FindFirst("Id")!.Value);
                var result = await _authService.ChangePassword(id, changePassword);
                return StatusCode(result.StatusCode, result);
            }
            else return StatusCode(401, "Please login first");

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromForm] string email)
        {
            var result = await _authService.ForgotPassword(email);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewPassword([FromBody] DataRequestUser_CreateNewPassword request)
        {
            var result = await _authService.CreateNewPassword(request);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> AddRolesToUser([FromRoute] Guid userId, [FromBody] List<string> roles)
        {
            var result = await _authService.AddRolesToUser(userId, roles);
            return StatusCode(result.StatusCode, result);
        }
        

        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> DeleteRoleOfUser([FromRoute] Guid userId, [FromBody] List<string> roles)
        {
            var result = await _authService.DeleteRoles(userId, roles);
            return StatusCode(result.StatusCode, result);
        }

    }
}
