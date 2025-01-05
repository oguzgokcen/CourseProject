using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.Service.Services.TokenManager;
using Microsoft.AspNetCore.Identity;
using System.Net;
using AutoMapper;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.ServiceDto_s.Requests.Login;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using FluentValidation;

namespace CourseApi.Service.Services.UserManager
{
	public class UserService(UserManager<AppUser> _userManager, ITokenService _tokenService,ICourseRepository _courseRepository, IValidator<RegisterRequest> _userValidator, IMapper _mapper) : IUserService
	{

		public async Task<BaseApiResponse<TokenDto>> UserLogin(LoginRequest loginRequest)
		{
			var user = await _userManager.FindByEmailAsync(loginRequest.Email);

			if (user == null)
			{
				return BaseApiResponse<TokenDto>.Error("User not found");
			}

			var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

			if (!result)
			{
				return BaseApiResponse<TokenDto>.Error("Invalid password");
			}

			var roles = await _userManager.GetRolesAsync(user);

			var token = _tokenService.GenerateAccessToken(user, roles);

			var refreshToken = _tokenService.GenerateRefreshToken(user);

			return BaseApiResponse<TokenDto>.Success(new TokenDto(token,refreshToken));
		}

		public async Task<BaseApiResponse<TokenDto>> UserRegister(RegisterRequest registerRequest)
		{
			var validationResult = await _userValidator.ValidateAsync(registerRequest);
			if (!validationResult.IsValid)
			{
				return BaseApiResponse<TokenDto>.Error(validationResult.Errors.First().ErrorMessage);
			}
			var user = new AppUser
			{
				Email = registerRequest.Email,
				UserName = registerRequest.Email,
				FullName = registerRequest.FullName
			};
			var result = await _userManager.CreateAsync(user, registerRequest.Password);
			if (!result.Succeeded)
			{
				return BaseApiResponse<TokenDto>.Error(result.Errors.First().Description);
			}

			var registeredUser = await _userManager.FindByEmailAsync(registerRequest.Email);

			var token = _tokenService.GenerateAccessToken(registeredUser, null);

			var refreshToken = _tokenService.GenerateRefreshToken(user);

			return BaseApiResponse<TokenDto>.Success(new TokenDto(token, refreshToken),(int)HttpStatusCode.Created);
		}

		public async Task<BaseApiResponse<TokenDto>> RefreshAcessToken(RefreshTokenRequest refreshTokenRequest)
		{
			var token = await _tokenService.RefreshAcessToken(refreshTokenRequest);
			if (token is null)
			{
				return BaseApiResponse<TokenDto>.Error("The refresh token is expired.");
			}

			return BaseApiResponse<TokenDto>.Success(token);
		}

		public async Task<BaseApiResponse<UserDetailDto>> GetUserProfileById(string id)
		{
			var result = await _userManager.FindByIdAsync(id);

			if (result == null)
			{
				return BaseApiResponse<UserDetailDto>.Error("User profile not found!");
			}

			return BaseApiResponse<UserDetailDto>.Success(_mapper.Map<UserDetailDto>(result));
		}

		public async Task<BaseApiResponse<string>> UpdateUserProfile(UpdateUserDetailDto userDetailDto,string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return BaseApiResponse<string>.Error("User not found");
			}
			user.FullName = userDetailDto.FullName;  //TODO : validator
			user.Email = userDetailDto.Email;
			user.Title = userDetailDto.Title;
			user.Website = userDetailDto.Website;
			user.Description = userDetailDto.Description;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				return BaseApiResponse<string>.Error(result.Errors.First().Description);
			}
			return BaseApiResponse<string>.Success("User profile successfully updated");
		}

		public async Task<BaseApiResponse<IEnumerable<GetCourseListDto>>> GetUserCourses(Guid userId)
		{
			return BaseApiResponse<IEnumerable<GetCourseListDto>>.Success(await _courseRepository.GetUserCourses(userId));
		}
	}
}
