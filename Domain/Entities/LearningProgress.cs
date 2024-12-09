namespace Domain.Entities
{
    public class LearningProgress : BaseEntity
    {
        public bool IsFinished { get; set; }
        public Guid RegisterStudyId { get; set; }
        public RegisterStudy? RegisterStudy {  get; set; }
        public Guid SubjectDetailId { get; set; }
        public SubjectDetail? SubjectDetail { get; set; }
    }
}
