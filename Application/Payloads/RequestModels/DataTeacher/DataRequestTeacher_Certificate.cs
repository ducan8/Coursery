
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Payloads.RequestModels.DataTeacher
{
    public class DataRequestTeacher_Certificate
    {
        [Required(ErrorMessage = "Name of certificate is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description of certificate is required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Image of certificate is required")]
        public IFormFile Image { get; set; } 
        [Required(ErrorMessage = "Type of certificate is required")]
        public string CertificateType { get; set; } = string.Empty;
    }
}
