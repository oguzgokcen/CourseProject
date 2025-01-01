using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.DataLayer.Repositories
{
	public interface ICourseRepository
	{
		Task<IEnumerable<GetCourseListDto>> GetCourses(SearchCourseRequest searchParams);
		Task<CourseDetailDto?> GetCourseDetailById(int id);
	}
}
