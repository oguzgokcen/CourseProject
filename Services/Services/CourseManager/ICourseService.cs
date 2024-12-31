using CourseApi.DataLayer.DataContext.Entities;
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
	public interface ICourseService
	{
		public Task<BaseApiResponse<IEnumerable<CourseResponseDto>>> GetSearchedCourses(SearchCourseRequest searchCourseRequest);

		public Task<BaseApiResponse<Course>> GetCourseById(int id);
	}
}
