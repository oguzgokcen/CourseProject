using AutoMapper;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.Service.Services.CourseManager
{
	public class CourseService(ICourseRepository _courseRepository,ICartRepository _cartRepository) : ICourseService
	{
		public async Task<BaseApiResponse<PaginatedResult>> GetSearchedCourses(SearchCourseRequest searchCourseRequest)
		{
			var courses = await _courseRepository.GetCourses(searchCourseRequest);

			if(courses.TotalCount == 0)
			{
				return BaseApiResponse<PaginatedResult>.Error("No course has been found for given parameters.");
			}

			return BaseApiResponse<PaginatedResult>.Success(courses);
		}

		public async Task<BaseApiResponse<CourseDetailDto>> GetCourseDetailById(int id)
		{
			var course = await _courseRepository.GetCourseDetailById(id);
			if (course == null)
			{
				return BaseApiResponse<CourseDetailDto>.Error("No course has been found");
			}
			return BaseApiResponse<CourseDetailDto>.Success(course);
		}

		public async Task<BaseApiResponse<IsCourseBoughtDto>> CheckIfCourseIsBought(int courseId, Guid userId)
		{
			var boughtCourseTime = await _courseRepository.CheckIfCourseIsBought(courseId, userId);
			var isOnCart = await _cartRepository.IsCourseExistsInUserCart(courseId, userId);
			return BaseApiResponse<IsCourseBoughtDto>.Success(new IsCourseBoughtDto(boughtCourseTime.HasValue,isOnCart,boughtCourseTime));

		}

	}
}
