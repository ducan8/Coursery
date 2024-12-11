namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Introduction { get; set; } = string.Empty;
        public string ImageCourse {  get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public double Price { get; set; }
        public int TotalCourseDuration { get; set; }
        public int NumberOfStudent {  get; set; }
        public int NumberOfPurchases { get; set; }
        public DateTime UpdateTime {  get; set; }
        public string Requirements { get; set; }

        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }
        public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}