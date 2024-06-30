using AutoMapper;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.BusinessLoginLayer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<SuperAdminDto, SuperAdmin>();
            CreateMap<AdminDto, Admin>();
            CreateMap<StudentDto, Student>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).ReverseMap();
            CreateMap<UpdateStudentDto, Student>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).ReverseMap();
            CreateMap<StudentLogsDto, StudentLogs>();
            CreateMap<StudentLogs, StudentLogsDto>()
                .ForMember(dest => dest.UpdateUserName, opt => opt.MapFrom(src => src.UpdateAdmin.AppUser.UserName))
                .ForMember(dest => dest.CreateUserName, opt => opt.MapFrom(src => src.CreateAdmin.AppUser.UserName));
            

        }
    }
}
