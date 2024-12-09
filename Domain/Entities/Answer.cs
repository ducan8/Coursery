using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Answers")]
    public class Answer : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public Guid? ParentAnswer {  get; set; }


        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
