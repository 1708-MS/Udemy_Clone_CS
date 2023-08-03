using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Application.DTO.ModelsDto;
using Udemy_WebApp.Application.DTO.UserRegisterationDto;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Application.DTO.MappingProfile
{
    /// <summary>
    /// The DtoMappingProfile is responsible for defining the mapping configurations between DTOs and the Domain Models using AutoMapper.
    /// AutoMapper is a popular object-to-object mapping library that simplifies the mapping process between different object types
    /// </summary>
    public class DtoMappingProfile : Profile
    {
        /// <summary>
        /// This is the constructor of the DtoMappingProfile class where the mapping configurations are defined of the Domain Models and Other Models
        /// </summary>
        public DtoMappingProfile()
        {
            CreateMap<UserRegistrationDto, ApplicationUser>()
                .ForMember(ApplicationUser => ApplicationUser.UserName, UserRegistrationDto => UserRegistrationDto.MapFrom(src=>src.RegistrationEmail))
                .ForMember(ApplicationUser => ApplicationUser.UserFullName, UserRegistrationDto => UserRegistrationDto.MapFrom(src => src.RegistrationUserName))
                .ForMember(ApplicationUser => ApplicationUser.Email, UserRegistrationDto => UserRegistrationDto.MapFrom(src => src.RegistrationEmail))
                .ForMember(ApplicationUser => ApplicationUser.Address, UserRegistrationDto => UserRegistrationDto.MapFrom(src => src.UserAddress))
                .ReverseMap();

            CreateMap<LoginDto, ApplicationUser>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryDto>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
          
        }
    }
}
