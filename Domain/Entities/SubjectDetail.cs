namespace Domain.Entities
{
    public class SubjectDetail : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        //public bool IsFinished { get; set; }
        public string LinkVideo { get; set; } = string.Empty;

        //public bool IsActive { get; set; }


        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public virtual ICollection<Question>? Question { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
        public virtual ICollection<Practice>? Practices { get; set; }

    }
}
