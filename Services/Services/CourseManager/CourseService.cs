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
	public class CourseService(ICourseRepository _courseRepository, IMapper _mapper) : ICourseService
	{
		public async Task<BaseApiResponse<IEnumerable<CourseResponseDto>>> GetSearchedCourses(SearchCourseRequest searchCourseRequest)
		{
			var courses = await _courseRepository.GetCourses(searchCourseRequest);

			if(!courses.Any())
			{
				return BaseApiResponse<IEnumerable<CourseResponseDto>>.Error("No course has been found for given parameters.");
			}

			return BaseApiResponse<IEnumerable<CourseResponseDto>>.Success(courses);
		}

		public async Task<BaseApiResponse<Course>> GetCourseById(int id)
		{
			var course = await _courseRepository.GetCourseById(id);
			if (course == null)
			{
				return BaseApiResponse<Course>.Error("No course has been found");
			}
			return BaseApiResponse<Course>.Success(course);
		}

	}
}
