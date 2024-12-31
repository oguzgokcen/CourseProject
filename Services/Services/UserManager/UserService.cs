using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.Service.Services.TokenManager;
using Microsoft.AspNetCore.Identity;
using System.Net;
namespace CourseApi.Service.Services.UserManager
{
	public class UserService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager, ITokenService _tokenService) : IUserService
	{

		public async Task<BaseApiResponse<string>> UserLogin(LoginRequest loginRequest)
		{
			var user = await _userManager.FindByEmailAsync(loginRequest.Email);

			if (user == null)
			{
				return BaseApiResponse<string>.Error("User not found");
			}

			var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

			if (!result)
			{
				return BaseApiResponse<string>.Error("Invalid password");
			}

			var roles = await _userManager.GetRolesAsync(user);

			var token = _tokenService.GenerateToken(user, roles);

			return BaseApiResponse<string>.Success(token);
		}

		public async Task<BaseApiResponse<string>> UserRegister(RegisterRequest registerRequest)
		{
			var user = new AppUser
			{
				Email = registerRequest.Email,
				UserName = registerRequest.Email,
				FullName = registerRequest.FullName
			};
			var result = await _userManager.CreateAsync(user, registerRequest.Password);
			if (!result.Succeeded)
			{
				return BaseApiResponse<string>.Error(result.Errors.First().Description);
			}
			return BaseApiResponse<string>.Success("User successfully created");
		}
	}
}
