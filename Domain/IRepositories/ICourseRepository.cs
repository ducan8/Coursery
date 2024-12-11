
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ICourseRepository
    {
        Task<Course> GetCourse(Guid courseId);
    }
}
