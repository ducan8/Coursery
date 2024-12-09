namespace Domain.Entities
{
    public class RegisterStudy : BaseEntity
    {
        public bool IsFinished { get; set; }
        public int PercentComplete { get; set; } = 0;
        public DateTime RegisterTime { get; set; } = DateTime.Now;
        public DateTime? CompletionTime { get; set; }
        //public bool IsActive { get; set; }


        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
        public virtual ICollection<DoHomework>? DoHomeworks { get; set; } 
    }
}
