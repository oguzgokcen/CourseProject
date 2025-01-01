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
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
			var response =await _userService.GetUserProfileById(userId);
			return ActionResultInstance(response);
		}


		[HttpPut("profile")]
		public async Task<IActionResult> UpdateUserProfile(UpdateUserDetailDto userDetailDto)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
			var response = await _userService.UpdateUserProfile(userDetailDto,userId);
			return ActionResultInstance(response);
		}
	}
}
