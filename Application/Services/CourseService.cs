using Application.IServices;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataCourse;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Course> _baseCourseRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseService(IMapper mapper,
                             IBaseRepository<Course> baseCourseRepository,
                             ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _baseCourseRepository = baseCourseRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ResponseObject<IEnumerable<Course>>> GetAllCourse()
        {
            var listCourse = await _baseCourseRepository.GetAllAsync();
            if (listCourse == null) return new ResponseObject<IEnumerable<Course>>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "We don't have any course.",
                Data = null
            };

            return new ResponseObject<IEnumerable<Course>>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "get all course successfully.",
                Data = listCourse
            };
        }

        public async Task<ResponseObject<DataResponseCourse>> GetCourseWithAllSubject(Guid courseId)
        {
            try
            {
                var course = await _courseRepository.GetCourse(courseId);

                if (course == null) return new ResponseObject<DataResponseCourse>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "You don't have any subject.",
                    Data = null
                };

                return new ResponseObject<DataResponseCourse>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get all courses successfully.",
                    Data = _mapper.Map<DataResponseCourse>(course)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseCourse>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
