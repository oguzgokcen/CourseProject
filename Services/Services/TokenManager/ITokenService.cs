using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests.Login;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Services.TokenManager
{
	public interface ITokenService
	{
		string GenerateAccessToken(AppUser appUser, IEnumerable<string>? roles);
		Task<TokenDto?> RefreshAcessToken(RefreshTokenRequest refreshTokenRequest);
		string GenerateRefreshToken(AppUser user);
	}
}
