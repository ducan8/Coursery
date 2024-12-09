
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsForCourse(Guid courseId);
    }
}
