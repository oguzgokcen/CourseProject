using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.Service.Services.CategoryManager
{
	public class CategoryService(ICategoryRepository _categoryRepository) : ICategoryService
	{
		public async Task<BaseApiResponse<IEnumerable<CategoryDto>>> GetAllKeywords()
		{
			var result = await _categoryRepository.GetAllKeywords();

			return BaseApiResponse<IEnumerable<CategoryDto>>.Success(result);
		}

		public async Task<BaseApiResponse<PaginatedResult>> GetCategoryCourses(SearchCourseByCategory searchParams)
		{
			var result = await _categoryRepository.GetCategoryCourses(searchParams);
			return BaseApiResponse<PaginatedResult>.Success(result);
		}
	}
}
