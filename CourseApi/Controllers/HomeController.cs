using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.Service.Services.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	public class HomeController(IUserService _userService) : BaseApiController
	{
		public IActionResult Index()
		{
			return Ok("Welcome to CourseApi");
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var result = await _userService.UserLogin(loginRequest);

			return ActionResultInstance(result);
		}

		[Authorize(Roles ="Teacher")]
		[HttpGet("Deneme")]
		public async Task<IActionResult> IsAuthorizedCheck(string lololo)
		{
			return ActionResultInstance(BaseApiResponse<string>.Success("a"));
		}
	}
}
