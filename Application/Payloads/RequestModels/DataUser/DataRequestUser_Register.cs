using System.ComponentModel.DataAnnotations;

namespace Application.Payloads.RequestModels.DataUser
{
    public class DataRequestUser_Register
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Phonenumber is required")]
        [Phone]
        public string PhoneNumber { get; set; } = String.Empty;

        public string FullName { get; set; } = String.Empty;
        
        public DateTime DateOfBirth { get; set; }
    }
}
