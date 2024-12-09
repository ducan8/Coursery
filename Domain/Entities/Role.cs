namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public virtual ICollection<Permission>? Permissions { get; set;} 
    }
}
