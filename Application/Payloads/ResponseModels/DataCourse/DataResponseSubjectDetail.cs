
using Domain.Entities;

namespace Application.Payloads.ResponseModels.DataCourse
{
    public class DataResponseSubjectDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LinkVideo { get; set; } = string.Empty;

        //public virtual ICollection<Question>? Question { get; set; }
        //public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
        //public virtual ICollection<Practice>? Practices { get; set; }
    }
}
