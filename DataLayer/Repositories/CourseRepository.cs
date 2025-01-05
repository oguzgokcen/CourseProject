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
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using Microsoft.AspNetCore.Identity;

namespace CourseApi.DataLayer.Repositories
{
	public class CourseRepository(CourseDbContext _dbContext,UserManager<AppUser> _userManager,IMapper _mapper) : ICourseRepository
	{
		public async Task<PaginatedResult> GetCourses(SearchCourseRequest searchParams)
		{
			var query = _dbContext.Courses.Include(x => x.Instructor).AsQueryable();
			if (!string.IsNullOrWhiteSpace(searchParams.Keyword))
			{
				var keyword = searchParams.Keyword.Trim().ToLower();
				query = query.Where(x => x.Title.ToLower().Contains(keyword));
			}

			if (searchParams.MinRating.HasValue)
			{
				query = query.Where(x => x.Rating >= searchParams.MinRating.Value);
			}

			if (searchParams.Language.HasValue)
			{
				query = query.Where(x => x.Language == searchParams.Language.Value);
			}

			var totalCountTask =await query.CountAsync();

			if (searchParams.PageNumber.HasValue && searchParams.PageSize.HasValue)
			{
				query = query.Skip((searchParams.PageNumber.Value - 1) * searchParams.PageSize.Value)
					.Take(searchParams.PageSize.Value);
			}

			var resultTask =await query.ProjectTo<GetCourseListDto>(_mapper.ConfigurationProvider).ToListAsync();

			return new PaginatedResult(resultTask, totalCountTask);
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

		public async Task<IEnumerable<GetCourseListDto>> GetTeachersCourses(Guid teacherId)
		{
			return await _dbContext.Courses.Include(x => x.Instructor).Where(x => x.InstructorId == teacherId).ProjectTo<GetCourseListDto>(_mapper.ConfigurationProvider).ToListAsync();
		}

	}
}
