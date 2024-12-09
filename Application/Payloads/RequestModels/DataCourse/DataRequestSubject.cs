using System.ComponentModel.DataAnnotations;

namespace Application.Payloads.RequestModels.DataCourse
{
    public class DataRequestSubject
    {
        [Required(ErrorMessage = "Name of subject is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Symbol is required")]
        public string Symbol { get; set; } = string.Empty;
        
    }
}
