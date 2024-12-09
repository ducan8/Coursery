using Application.IServices;
using Application.Payloads.RequestModels.DataTeacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        

        [HttpPost]
        public async Task<IActionResult> RegisterTeacher([FromForm] DataRequestTeacher_Certificate request)
        {
            var result = await _teacherService.RegisterTeacher(request);
            return StatusCode(result.StatusCode, result);
        }

        //[HttpPost]
        //public async Task<IActionResult> ReviewTeacher(Guid userId)
        //{

        //}
    }
}
