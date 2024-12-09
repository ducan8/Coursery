namespace Domain.Entities
{
    public class TestCase : BaseEntity
    {
       public string Input { get; set; } = string.Empty;
        public string Output { get; set; } = string.Empty;

        public Guid LanguageProgramingId { get; set; }
        public LanguagePrograming? LanguagePrograming { get; set; }
        public Guid PracticeId { get; set; }
        public Practice Practice { get; set; }

        public virtual ICollection<RunTestCase>? RunTestCases { get; set; }
    }
}
