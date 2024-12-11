using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataCourse;
using Domain.Entities;

namespace Application.IServices
{
    public interface ICourseService
    {
        Task<ResponseObject<IEnumerable<Course>>> GetAllCourse();
        Task<ResponseObject<DataResponseCourse>> GetCourseWithAllSubject(Guid courseId);

    }
}
