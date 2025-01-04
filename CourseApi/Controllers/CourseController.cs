using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.Service.Services.CourseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	public class CourseController(ICourseService _courseService) : BaseApiController
	{
		[HttpGet("search")]
		public async Task<IActionResult> GetCourses([FromQuery] SearchCourseRequest searchCourseRequest)
		{
			var result = await _courseService.GetSearchedCourses(searchCourseRequest);

			return ActionResultInstance(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCourseDetailById(int id)
		{
			var result = await _courseService.GetCourseDetailById(id);
			return ActionResultInstance(result);
		}




		[HttpGet("checkifbought/{id}")]
		[Authorize]
		public async Task<IActionResult> CheckIfCourseIsBought(int id)
		{
			var response = await _courseService.CheckIfCourseIsBought(id, UserId!.Value);
			return ActionResultInstance(response);
		}
	}
}

