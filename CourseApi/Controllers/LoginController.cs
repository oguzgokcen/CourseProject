using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Login;
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

		[HttpPost("GetAccessToken")]
		public async Task<IActionResult> GetAccessTokenFromRefreshToken(RefreshTokenRequest refreshTokenRequest)
		{
			var result = await _userService.RefreshAcessToken(refreshTokenRequest);
			return ActionResultInstance(result);
		}
	}
}
