using Domain.Enumerates;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = String.Empty;  
        public string FullName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public string Avatar { get; set; } = String.Empty;
        public ConstantEnums.UserStatus UserStatus { get; set; } = ConstantEnums.UserStatus.UnActived;
        public bool IsActive { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Instruction {  get; set; } = String.Empty;

        public virtual ICollection<Certificate>? Certificates { get; set; } 
        public virtual ICollection<Permission>? Permissions { get; set; } 
        public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; } 
        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<DoHomework>? DoHomework { get; set;}
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
        public virtual ICollection<CommentBlog>? CommentBlogs { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }
        public virtual ICollection<LikeBlog>? LikeBlogs { get; set; }

    }
}
