
using Application.Payloads.RequestModels.DataCourse;
using Application.Payloads.RequestModels.DataTeacher;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using Domain.Entities;

namespace Application.IServices
{
    public interface ITeacherService
    {
        Task<ResponseObject<DataResponseUser>> RegisterTeacher(DataRequestTeacher_Certificate certificate);

        //Task<ResponseObject<string>> ReviewTeacher(Guid teacherId, bool IsApprove);

        Task<ResponseObject<IEnumerable<Course>>> GetAllCourseForTeacher();
        Task<ResponseObject<string>> CreateCourse(DataRequestCourse newCourse);
        Task<ResponseObject<string>> UpdateCourse(Guid courseId, DataRequestCourse course);
        Task<ResponseObject<string>> DeleteCourse(Guid courseId);


        Task<ResponseObject<string>> CreateSubject(Guid courseId, DataRequestSubject subject);
        Task<ResponseObject<string>> UpdateSubject(Guid subjectId, DataRequestSubject subject);
        Task<ResponseObject<string>> DeleteSubject(Guid subjectId);
    }
}
