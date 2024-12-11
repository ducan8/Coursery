using Domain.Entities;

namespace Application.Payloads.ResponseModels.DataCourse
{
    public class DataResponseSubject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Symbol { get; set; }
        public virtual ICollection<DataResponseSubjectDetail>? SubjectDetails { get; set; }
        //public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }
        //public virtual ICollection<LearningProgress>? LearningProgress { get; set; }
    }
}
