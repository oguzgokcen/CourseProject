using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;

namespace CourseApi.DataLayer.RequestHelpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Course, CourseDetailDto>()
				.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
				.ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor))
				.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.ToString()));

			CreateMap<Course, GetCourseListDto>()
				.ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor));

			CreateMap<AppUser, UserDetailDto>();

			CreateMap<CategoryKeywords, CategoryDto>();

			CreateMap<AppUser, InstructorDto>();

			CreateMap<InstructorDetail, InstructorDto>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.InstructorUser.FullName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.InstructorUser.Email));

			CreateMap<PaymentLog, PaymentHistoryDto>();

			CreateMap<BoughtCourse, PaymentHistoryItemDto>()
				.ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
				.ForMember(dest => dest.BoughtDate, opt => opt.MapFrom(src => src.BoughtDate));
		}
	}
}

