
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories 
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Subject>> GetAllSubjectsForCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

    }
}
