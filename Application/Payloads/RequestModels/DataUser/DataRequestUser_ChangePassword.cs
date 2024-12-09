namespace Application.Payloads.RequestModels.DataUser
{
    public class DataRequestUser_ChangePassword
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
