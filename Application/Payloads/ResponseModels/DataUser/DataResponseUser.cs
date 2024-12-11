using Domain.Enumerates;

namespace Application.Payloads.ResponseModels.DataUser
{
    public class DataResponseUser : DataResponseBase
    {
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Avatar { get; set; } = String.Empty;
        public string Instruction {  get; set; } = String.Empty;
        public string UserStatus { get; set; } = ConstantEnums.UserStatus.UnActived.ToString();
    }
}
