
namespace Domain.Entities
{
    public class Blog : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }

        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }
        public virtual ICollection<LikeBlog>? LikeBlogs { get; set; }
        public virtual ICollection<CommentBlog>? CommentBlogs { get; set; }
    }
}
