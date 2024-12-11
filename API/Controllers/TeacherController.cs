using Application.IServices;
using Application.Payloads.RequestModels.DataCourse;
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

        #region course controller

        [HttpPost]
        public async Task<IActionResult> RegisterTeacher([FromForm] DataRequestTeacher_Certificate request)
        {
            var result = await _teacherService.RegisterTeacher(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourseForTeacher()
        {
            var result = await _teacherService.GetAllCourseForTeacher();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] DataRequestCourse newCourse)
        {
            var result = await _teacherService.CreateCourse(newCourse);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{courseId:Guid}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] Guid courseId, DataRequestCourse updateCourse)
        {
            var result = await _teacherService.UpdateCourse(courseId, updateCourse);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            var result = await _teacherService.DeleteCourse(courseId);
            return StatusCode(result.StatusCode, result);
        }
        #endregion


        #region subject controller

        

        [HttpPost("{courseId:Guid}")]
        public async Task<IActionResult> CreateSubject([FromRoute]Guid courseId, [FromForm] DataRequestSubject newsubject)
        {
            var result = await _teacherService.CreateSubject(courseId, newsubject);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{subjectId:Guid}")]
        public async Task<IActionResult> UpdateSubject([FromRoute] Guid subjectId, DataRequestSubject updatesubject)
        {
            var result = await _teacherService.UpdateSubject(subjectId, updatesubject);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubject(Guid subjectId)
        {
            var result = await _teacherService.DeleteSubject(subjectId);
            return StatusCode(result.StatusCode, result);
        }

        #endregion

    }
}
