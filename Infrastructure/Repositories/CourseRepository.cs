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

        public async Task<Course> GetCourse(Guid courseId)
        {
            var course = await _context.Courses
                                        .Include(x => x.Creator)
                                       .Include(x => x.Subjects)
                                       .ThenInclude(x => x.SubjectDetails)
                                       .FirstOrDefaultAsync(x => x.Id == courseId);

            return course;
        }
    }
}
