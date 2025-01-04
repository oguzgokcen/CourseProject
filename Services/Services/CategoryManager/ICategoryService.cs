using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Services.CategoryManager
{
	public interface ICategoryService
	{
		Task<BaseApiResponse<IEnumerable<CategoryDto>>> GetAllKeywords();
		Task<BaseApiResponse<PaginatedResult>> GetCategoryCourses(SearchCourseByCategory searchParams);
	}
}
