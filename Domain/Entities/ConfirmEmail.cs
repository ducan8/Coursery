namespace Domain.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public string ConfirmCode { get; set; } = String.Empty;
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime ExpiryTime {  get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
