using System.Security.Claims;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using CourseApi.Service.Services.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	[Authorize]
	public class UserController(IUserService _userService) : BaseApiController
	{
		[HttpGet("profile")]
		public async Task<IActionResult> GetUserProfile()
		{
			var response =await _userService.GetUserProfileById(UserId!.Value.ToString());
			return ActionResultInstance(response);
		}


		[HttpPut("profile")]
		public async Task<IActionResult> UpdateUserProfile(UpdateUserDetailDto userDetailDto)
		{
			var response = await _userService.UpdateUserProfile(userDetailDto,UserId!.Value.ToString());
			return ActionResultInstance(response);
		}

		[HttpGet("profile/course")]
		public async Task<IActionResult> GetUserCourses()
		{
			var courses = await _userService.GetUserCourses(UserId!.Value);
			return ActionResultInstance(courses);
		}

		[HttpGet("teacher/courses")]
		[Authorize(Roles = "Teacher")]
		public async Task<IActionResult> GetTeachersCourses()
		{
			var courses = await _userService.GetTeachersCourses(UserId!.Value);
			return ActionResultInstance(courses);
		}

		[HttpGet("payment-history")]
		public async Task<IActionResult> GetPaymentHistory()
		{
			var paymentHistory = await _userService.GetPaymentHistory(UserId!.Value);
			return ActionResultInstance(paymentHistory);
		}
	}
}
