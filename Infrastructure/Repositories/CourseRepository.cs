using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCourse(Guid courseId)
        {
            var listSubjects = await _context.Courses.Include(x => x.Subjects).ThenInclude(x => x.SubjectDetails).Where(x => x.Id == courseId).ToListAsync();

            return listSubjects;
        }
    }
}
