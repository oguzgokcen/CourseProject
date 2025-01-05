using CourseApi.DataLayer.CommonModels;
using CourseApi.DataLayer.DataContext.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.ServiceDto_s.Requests.Login;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CourseApi.Service.Services.TokenManager
{
	public class TokenService(IOptions<TokenOption> _tokenOptions,CourseDbContext _dbContext, UserManager<AppUser> _userManager) : ITokenService
	{
		public string GenerateAccessToken(AppUser appUser, IEnumerable<string>? roles)
		{
			var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.Value.AccessTokenExpiration);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Value.SecurityKey));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(GetClaims(appUser,_tokenOptions.Value.Audience, roles)),
				Expires = accessTokenExpiration,
				SigningCredentials = credentials,
				Issuer = _tokenOptions.Value.Issuer
			};

			var handler = new JsonWebTokenHandler();

			string accessToken = handler.CreateToken(tokenDescriptor);

			return accessToken;
		}

		public async Task<TokenDto?> RefreshAcessToken(RefreshTokenRequest refreshTokenRequest)
		{
			var refreshToken = await _dbContext.RefreshTokens.Include(x => x.User)
				.FirstOrDefaultAsync(r => r.Token == refreshTokenRequest.refreshToken);

			if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
			{
				return null;
			}

			var roles = await _userManager.GetRolesAsync(refreshToken.User);

			var accessToken =GenerateAccessToken(refreshToken.User, roles);

			refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddMinutes(_tokenOptions.Value.RefreshTokenExpiration);

			await _dbContext.SaveChangesAsync();

			return new TokenDto(accessToken, refreshToken.Token);
		}

		public string GenerateRefreshToken(AppUser user)
		{
			var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.Value.RefreshTokenExpiration);

			var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

			_dbContext.RefreshTokens.Add(new RefreshToken
			{
				Id = Guid.NewGuid(),
				UserId = user.Id,
				Token = refreshToken,
				ExpiresOnUtc = refreshTokenExpiration
			});

			_dbContext.SaveChangesAsync();

			return refreshToken;
		}

		private IEnumerable<Claim> GetClaims(AppUser appUser,List<string> audiences, IEnumerable<string>? appRoles)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
				new Claim(JwtRegisteredClaimNames.Name, appUser.FullName),
			};

			claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));

			if (appRoles != null && appRoles.Any())
			{
				claims.AddRange(appRoles.Select(appRole => new Claim(ClaimTypes.Role, appRole)));
			}

			return claims;
		}
	}
}
