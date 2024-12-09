using Domain.Enumerates;

namespace Domain.Entities
{
    public class Practice : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string PracticeCode { get; set; } = string.Empty;
        public string ExpectOutput { get; set; } = string.Empty;
        public ConstantEnums.Level Level { get; set; } = ConstantEnums.Level.Easy;
        public double MediumScore { get; set; }
        public bool IsRequired { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; }


        public Guid LanguageProgramingId { get; set; }
        public LanguagePrograming? LanguagePrograming { get; set; }
        public Guid SubjectDetailId { get; set; }
        public SubjectDetail? SubjectDetail { get; set; }
        public virtual ICollection<TestCase>? TestCases { get; set; }

    }
}
