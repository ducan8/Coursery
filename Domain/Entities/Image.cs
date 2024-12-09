
namespace Domain.Entities
{
    public class Image
    {
        public string Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Url { get; set; }

        public Certificate? Owner { get; set; }

    }
}
