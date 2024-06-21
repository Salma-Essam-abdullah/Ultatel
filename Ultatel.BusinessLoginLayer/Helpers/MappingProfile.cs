﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.BusinessLoginLayer.Dtos;
using Ultatel.Models.Entities;
using Ultatel.Models.Entities.Identity;

namespace Ultatel.BusinessLoginLayer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<AdminDto, Admin>();
            CreateMap<StudentDto, Student>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).ReverseMap();
            CreateMap<UpdateStudentDto, Student>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).ReverseMap();
            CreateMap<StudentLogsDto, StudentLogs>()
            .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => new AppUser { UserName = src.UserName }));

            // Reverse mapping from StudentLogs to StudentLogsDto
            CreateMap<StudentLogs, StudentLogsDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

        }
    }
}