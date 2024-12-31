using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Course;
using CourseApi.Service.Services.CourseManager;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	public class CourseController(ICourseService _courseService) : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> GetCourses([FromQuery] SearchCourseRequest searchCourseRequest)
		{
			var result = await _courseService.GetSearchedCourses(searchCourseRequest);

			return ActionResultInstance(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCourseById(int id)
		{
			var result = await _courseService.GetCourseById(id);
			return ActionResultInstance(result);
		}
}
}

