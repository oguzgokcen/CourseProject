using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using Microsoft.AspNetCore.Identity;

namespace CourseApi.DataLayer.Repositories
{
	public class CourseRepository(CourseDbContext _dbContext,UserManager<AppUser> _userManager,IMapper _mapper) : ICourseRepository
	{
		public async Task<IEnumerable<GetCourseListDto>> GetCourses(SearchCourseRequest searchParams)
		{
			var keyword = searchParams.Keyword.Trim().ToLower();
			var query = _dbContext.Courses.Include(x => x.Instructor)
				.Where(x => x.Title.ToLower().Contains(keyword));

			if (searchParams.PageNumber.HasValue && searchParams.PageSize.HasValue)
			{
				query = query.Skip((searchParams.PageNumber.Value - 1) * searchParams.PageSize.Value)
							 .Take(searchParams.PageSize.Value);
			}

			return await query.ProjectTo<GetCourseListDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<CourseDetailDto?> GetCourseDetailById(int id)
		{
			return await _dbContext.Courses.Include(x=>x.Categories).Include(x=> x.Instructor).ProjectTo<CourseDetailDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> AddCoursesToUser(IEnumerable<Course> cartCourses,Guid userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null)
			{
				return false;
			}

			user.BoughtCourses.AddRange(cartCourses);
			return true;
		}

		public async Task<IEnumerable<GetCourseListDto>> GetUserCourses(Guid userId)
		{
			return await _dbContext.BoughtCourses.Include(x=>x.Course).ThenInclude(x=>x.Instructor).Where(x => x.UserId == userId).Select(x=>x.Course).ProjectTo<GetCourseListDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<DateTime?> CheckIfCourseIsBought(int courseId, Guid userId)
		{
			return await _dbContext.BoughtCourses
				.Where(x => x.UserId == userId && x.CourseId == courseId).Select(x=>x.BoughtDate).FirstOrDefaultAsync();
		}

	}
}
