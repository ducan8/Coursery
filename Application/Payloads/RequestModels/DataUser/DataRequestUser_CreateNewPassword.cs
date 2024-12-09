namespace Application.Payloads.RequestModels.DataUser
{
    public class DataRequestUser_CreateNewPassword
    {
        public string ConfirmCode { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
