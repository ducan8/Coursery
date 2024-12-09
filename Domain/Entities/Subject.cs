namespace Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public virtual ICollection<SubjectDetail>? SubjectDetails { get; set; }
        public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgress { get; set; }
    }
}