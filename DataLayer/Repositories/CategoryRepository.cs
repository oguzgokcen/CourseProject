using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataLayer.Repositories
{
	public class CategoryRepository(CourseDbContext _dbContext,IMapper _mapper) : ICategoryRepository
	{
		public async Task<IEnumerable<CategoryDto>> GetAllKeywords()
		{
			return await _dbContext.CategoryKeywords.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<PaginatedResult> GetCategoryCourses(SearchCourseByCategory searchParams)
		{
			var searchTerm = searchParams.SearchTerm.Trim().ToLower();
			var query = _dbContext.Courses.Include(x => x.Instructor).Include(x => x.Categories)
				.Where(x => x.Categories.Any(y => y.SearchTerm.Equals(searchTerm)));
			if(searchParams.MinRating.HasValue)
			{
				query=query.Where(x=>x.Rating>=searchParams.MinRating.Value);
			}
			if(searchParams.Language.HasValue)
			{
				query=query.Where(x=>x.Language==searchParams.Language.Value);
			}
			var totalCount = await query.CountAsync();
			if (searchParams.PageNumber.HasValue && searchParams.PageSize.HasValue)
			{
				query = query.Skip((searchParams.PageNumber.Value - 1) * searchParams.PageSize.Value).Take(searchParams.PageSize.Value);
			}
			var courses = await query.ProjectTo<GetCourseListDto>(_mapper.ConfigurationProvider).ToListAsync();

			return new PaginatedResult(courses, totalCount);
		}
	}
}
