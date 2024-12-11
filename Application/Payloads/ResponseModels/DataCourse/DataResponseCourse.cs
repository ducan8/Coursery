using Domain.Entities;

namespace Application.Payloads.ResponseModels.DataCourse
{
    public class DataResponseCourse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Introduction { get; set; } 
        public string ImageCourse { get; set; }
        public string Code { get; set; } 
        public double Price { get; set; }
        public int TotalCourseDuration { get; set; }
        public int NumberOfStudent { get; set; }
        public int NumberOfPurchases { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Requirements { get; set; }

        public User Creator { get; set; }
        public ICollection<DataResponseSubject>? Subjects { get; set; }
        //public ICollection<RegisterStudy>? RegisterStudies { get; set; }
        //public ICollection<Bill>? Bills { get; set; }
    }
}
