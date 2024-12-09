

namespace Domain.Entities
{
    public class Documents : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public byte[] Content { get; set; } = new byte[0];
        public DateTime UploadTime { get; set; } = DateTime.Now;

        public Guid SubjectDetailId { get; set; }
        public SubjectDetail? SubjectDetail { get; set; }

    }
}
