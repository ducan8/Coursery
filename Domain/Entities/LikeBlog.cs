namespace Domain.Entities
{
    public class LikeBlog : BaseEntity
    {
        public bool UnLike { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } 
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
