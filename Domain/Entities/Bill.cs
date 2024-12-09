namespace Domain.Entities
{
    public class Bill : BaseEntity
    {
        public double Price { get; set; }
        public string TradingCode { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public Guid BillStatusId { get; set; }
        public BillStatus? BillStatus { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
