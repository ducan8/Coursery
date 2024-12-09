
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourse(Guid courseId);
    }
}
