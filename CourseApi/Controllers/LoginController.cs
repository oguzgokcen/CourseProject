using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.Service.Services.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	[AllowAnonymous]
	public class LoginController(IUserService _userService) : BaseApiController
	{

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var result = await _userService.UserLogin(loginRequest);

			return ActionResultInstance(result);
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterRequest registerRequest)
		{
			var result = await _userService.UserRegister(registerRequest);
			return ActionResultInstance(result);
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			// todo add teacher role.
			return Ok("Welcome to CourseApi");
		}
	}
}
