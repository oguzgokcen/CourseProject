using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.DataLayer.RequestHelpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Course, CourseResponseDto>()
				.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
				.ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor))
				.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));

			CreateMap<CategoryKeywords, CategoryDto>();
			CreateMap<AppUser, InstructorDto>();
		}
	}
}
