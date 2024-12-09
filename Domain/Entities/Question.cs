namespace Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int NumberOfAnswers { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }


        public Guid SubjectDetailId { get; set; }
        public SubjectDetail? SubjectDetail { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public virtual ICollection<Answer>? Answers { get; set; }
    }
}
