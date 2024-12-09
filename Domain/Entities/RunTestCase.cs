namespace Domain.Entities
{
    public class RunTestCase : BaseEntity
    {
        public string Result { get; set; } = string.Empty;
        public double RunTime { get; set; }

        public Guid DoHomeworkId { get; set; }
        public DoHomework? DoHomework { get; set; }
        public Guid TestCaseId { get; set; }
        public TestCase TestCase { get; set; }
    }
}