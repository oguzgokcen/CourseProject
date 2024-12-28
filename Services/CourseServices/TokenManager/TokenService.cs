using CourseApi.DataLayer.CommonModels;
using CourseApi.DataLayer.DataContext.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
namespace CourseApi.Service.CourseServices.TokenManager
{
	public class TokenService(IOptions<TokenOption> _tokenOptions) : ITokenService
	{
		public string GenerateToken(AppUser appUser, IEnumerable<string>? roles)
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

			string token = handler.CreateToken(tokenDescriptor);

			return token;
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
			//(appRoles != null && 
			if (appRoles.Any())
			{
				claims.AddRange(appRoles.Select(appRole => new Claim(ClaimTypes.Role, appRole)));
			}

			return claims;
		}
	}
}
