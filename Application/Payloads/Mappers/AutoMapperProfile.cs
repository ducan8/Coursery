using Application.Payloads.RequestModels.DataCourse;
using Application.Payloads.RequestModels.DataTeacher;
using Application.Payloads.RequestModels.DataUser;
using Application.Payloads.ResponseModels.DataCourse;
using Application.Payloads.ResponseModels.DataUser;
using AutoMapper;
using Domain.Entities;
using BcryptNet = BCrypt.Net.BCrypt;

namespace Application.Payloads.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<DataRequestUser_Register, User>()
                .ForMember(d => d.Password, opt => opt.MapFrom(s => BcryptNet.HashPassword(s.Password)));
            CreateMap<User, DataResponseUser>()
                .ForMember(d => d.UserStatus, opt => opt.MapFrom(s => s.UserStatus.ToString()));

            CreateMap<DataRequestTeacher_Certificate, Certificate>();

            CreateMap<DataRequestCourse,  Course>();

            CreateMap<DataRequestSubject, Subject>();

            CreateMap<Course, DataResponseCourse>();
            CreateMap<Subject, DataResponseSubject>();
            CreateMap<SubjectDetail, DataResponseSubjectDetail>();

        }
    }
}
