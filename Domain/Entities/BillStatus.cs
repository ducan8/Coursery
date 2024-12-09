namespace Domain.Entities
{
    public class BillStatus : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}