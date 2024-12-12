using Application.IServices;
using Application.Payloads.RequestModels.DataCourse;
using Application.Payloads.RequestModels.DataTeacher;
using Application.Payloads.Response;
using Application.Payloads.ResponseModels.DataUser;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthService _authService;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;
        private readonly IBaseRepository<Certificate> _baseCertificateRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Image> _baseImageRepository;
        private readonly IBaseRepository<Course> _baseCourseRepository;
        private readonly IBaseRepository<Subject> _baseSubjectRepository;
        private readonly ICourseRepository _courseRepository;

        public TeacherService(IMapper mapper,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthService authService,
                               IPhotoService photoService,
                               IUserService userService,
                               IBaseRepository<Certificate> baseCertificateRepository,
                               IBaseRepository<User> baseUserRepository,
                               IBaseRepository<Image> baseImageRepository,
                               IBaseRepository<Course> baseCourseRepository,
                               IBaseRepository<Subject> baseSubjectRepository,
                               ICourseRepository courseRepository
            )
        {
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
            _authService = authService;
            _photoService = photoService;
            _userService = userService;
            _baseCertificateRepository = baseCertificateRepository;
            _baseUserRepository = baseUserRepository;
            _baseImageRepository = baseImageRepository;
            _baseCourseRepository = baseCourseRepository;
            _baseSubjectRepository = baseSubjectRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ResponseObject<string>> CreateCourse(DataRequestCourse newCourse)
        {
            try
            {
                var user = _userService.GetCurrentUser();

                if (!user.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };
                if (string.IsNullOrEmpty(user.FindFirst("Certificate").Value)) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have certificate to create a course yet.",
                    Data = null
                };

                Course course = _mapper.Map<Course>(newCourse);
                course.CreatorId = Guid.Parse(user.FindFirst("Id")!.Value);
                if (course.ImageCourse != null)
                {
                    string image = await _photoService.AddPhotoAsync(newCourse.ImageCourse);
                    course.ImageCourse = image;
                }

                course = await _baseCourseRepository.CreateAsync(course);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Created a new course successfully",
                    Data = ""
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = ex.StackTrace
                };
            }
        }

        public async Task<ResponseObject<string>> UpdateCourse(Guid courseId, DataRequestCourse updateCourse)
        {
            try
            {
                var user = _userService.GetCurrentUser();

                if (!user.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var courseInDB = await _baseCourseRepository.GetByIdAsync(x => x.Id == courseId);

                if (courseInDB == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "The course doesn't exists.",
                    Data = null
                };

                if (updateCourse.ImageCourse != null)
                {
                    string image = await _photoService.AddPhotoAsync(updateCourse.ImageCourse);
                    courseInDB.ImageCourse = image;
                }

                courseInDB.Code = updateCourse.Code;
                courseInDB.Name = updateCourse.Name;
                courseInDB.Introduction = updateCourse.Introduction;
                courseInDB.Price = updateCourse.Price;
                courseInDB.UpdateTime = DateTime.Now;

                await _baseCourseRepository.UpdateAsync(courseInDB);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Updated the course successfully",
                    Data = ""
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = ex.StackTrace
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<Course>>> GetAllCourseForTeacher()
        {
            try
            {
                var currentUser = _userService.GetCurrentUser();

                if (!currentUser.IsInRole("Teacher")) return new ResponseObject<IEnumerable<Course>>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var listCourse = await _baseCourseRepository.GetAllAsync(x => x.CreatorId == Guid.Parse(currentUser.FindFirst("Id")!.Value));

                if (listCourse == null) return new ResponseObject<IEnumerable<Course>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "You don't have any course.",
                    Data = null
                };

                return new ResponseObject<IEnumerable<Course>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Get all courses successfully.",
                    Data = listCourse.ToList()
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<IEnumerable<Course>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser>> RegisterTeacher(DataRequestTeacher_Certificate request_certificate)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser();

                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Message = "Something went wrong. Please sign in again.",
                        Data = null
                    };
                }

                var user = await _baseUserRepository.GetByIdAsync(Guid.Parse(currentUser.FindFirst("Id")!.Value));

                if (user == null) return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Something went wrong. Please sign in again.",
                    Data = null
                };

                var certificate = _mapper.Map<Certificate>(request_certificate);
                certificate.UserId = user.Id;

                certificate = await _baseCertificateRepository.CreateAsync(certificate);

                var imageCert = await _photoService.AddPhotoAsync(request_certificate.Image);

                if (imageCert == null) return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Can't upload your certificate's photo. Please try again.",
                    Data = null
                };

                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Successfully registered as a teacher. Please wait for approval.",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> DeleteCourse(Guid courseId)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser();

                if (!currentUser.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var course = await _baseCourseRepository.GetAllAsync(x => x.Id == courseId);

                if (course == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "You don't have this course.",
                    Data = null
                };

                await _baseCourseRepository.DeleteAsync(courseId);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Delete this course successfully.",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> ReviewTeacher(Guid userId, bool isApprove)
        {
            try
            {
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Something went wrong. Please sign in again.",
                    Data = null
                };

                if (isApprove)
                {
                    await _authService.AddRolesToUser(userId, new List<string> { "Teacher" });
                    return new ResponseObject<string>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Accept success.",
                        Data = null
                    };
                }

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Refuse success.",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }

        }




        public async Task<ResponseObject<string>> CreateSubject(Guid courseId, DataRequestSubject subject)
        {
            try
            {
                var user = _userService.GetCurrentUser();

                if (!user.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var course = await _baseCourseRepository.GetByIdAsync(courseId);
                if (course == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Your course doesn't exists.",
                    Data = null
                };

                var newSubject = _mapper.Map<Subject>(subject);
                newSubject.CourseId = courseId;

                await _baseSubjectRepository.CreateAsync(newSubject);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Create a new subject successfully",
                    Data = ""
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<string>> UpdateSubject(Guid subjectId, DataRequestSubject updateSubject)
        {
            try
            {
                var user = _userService.GetCurrentUser();

                if (!user.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var subjectInDB = await _baseSubjectRepository.GetByIdAsync(x => x.Id == subjectId);

                if (subjectInDB == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "The course doesn't exists.",
                    Data = null
                };

                subjectInDB.Name = updateSubject.Name;
                subjectInDB.Symbol = updateSubject.Symbol;

                await _baseSubjectRepository.UpdateAsync(subjectInDB);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Updated the subject successfully",
                    Data = ""
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = ex.StackTrace
                };
            }
        }

        public async Task<ResponseObject<string>> DeleteSubject(Guid subjectId)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser();

                if (!currentUser.IsInRole("Teacher")) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You don't have permisison to do this action.",
                    Data = null
                };

                var subject = await _baseCourseRepository.GetAllAsync(x => x.Id == subjectId);

                if (subject == null) return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "You don't have this subject.",
                    Data = null
                };

                await _baseSubjectRepository.DeleteAsync(subjectId);

                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Delete this subject successfully.",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
