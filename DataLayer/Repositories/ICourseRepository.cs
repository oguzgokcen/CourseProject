using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses;

namespace CourseApi.DataLayer.Repositories
{
	public interface ICourseRepository
	{
		Task<PaginatedResult> GetCourses(SearchCourseRequest searchParams);
		Task<CourseDetailDto?> GetCourseDetailById(int id);
		Task<bool> AddCoursesToUser(IEnumerable<Course> cartCourses, Guid userId);
		Task<IEnumerable<GetCourseListDto>> GetUserCourses(Guid userId);
		Task<DateTime?> CheckIfCourseIsBought(int courseId, Guid userId);
	}
}
