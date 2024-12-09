using Domain.Enumerates;

namespace Domain.Entities
{
    public class DoHomework : BaseEntity
    {
        public ConstantEnums.HomeworkStatus Status { get; set; } = ConstantEnums.HomeworkStatus.Pending;
        public bool IsFinished { get; set; }
        public string ActualOutput { get; set; } = string.Empty;
        public DateTime CompletionTime {  get; set; }


        public Guid RegisterStudyId { get; set; }
        public RegisterStudy RegisterStudy { get; set; }
        public Guid PracticeId { get; set; }
        public Practice? Practice { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
