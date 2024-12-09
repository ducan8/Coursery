
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Payloads.RequestModels.DataCourse
{
    public class DataRequestCourse
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Introduce is required")]
        public string Introduce { get; set; } 

        public IFormFile? ImageCourse { get; set; } 

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } 

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
    }
}
