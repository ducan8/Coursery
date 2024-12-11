using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseService.GetAllCourse();
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpGet("{courseId:Guid}")]
        public async Task<IActionResult> GetCourseWithAllSubject([FromRoute] Guid courseId)
        {
            var result = await _courseService.GetCourseWithAllSubject(courseId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
