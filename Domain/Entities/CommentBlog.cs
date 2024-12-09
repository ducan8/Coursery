namespace Domain.Entities
{
    public class CommentBlog : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public bool Edited { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
