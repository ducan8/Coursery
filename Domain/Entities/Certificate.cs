namespace Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CertificateType { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Image>? Images { get; set; }  
    }
}
