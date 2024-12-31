using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.DataLayer.Repositories
{
	public class CourseRepository(CourseDbContext _dbContext,IMapper _mapper) : ICourseRepository
	{
		public async Task<IEnumerable<CourseResponseDto>> GetCourses(SearchCourseRequest searchParams)
		{
			var query = _dbContext.Courses.Include(x=> x.Categories).Include(x=> x.Instructor)
				.Where(x => x.Title.ToLower().Contains(searchParams.Keyword.ToLower()));

			if(searchParams.PageNumber.HasValue && searchParams.PageSize.HasValue)
			{
				query = query.Skip(searchParams.PageNumber.Value).Take(searchParams.PageSize.Value);
			}
			return await query.ProjectTo<CourseResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<Course?> GetCourseById(int id)
		{
			return await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
		}

	}
}
