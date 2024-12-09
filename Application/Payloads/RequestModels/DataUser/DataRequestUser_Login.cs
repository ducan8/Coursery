using System.ComponentModel.DataAnnotations;

namespace Application.Payloads.RequestModels.DataUser
{
    public class DataRequestUser_Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        
    }
}
