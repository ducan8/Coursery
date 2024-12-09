namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public bool IsSeen { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
