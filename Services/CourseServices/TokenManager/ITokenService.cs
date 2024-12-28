using CourseApi.DataLayer.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.CourseServices.TokenManager
{
	public interface ITokenService
	{
		string GenerateToken(AppUser appUser, IEnumerable<string>? roles);
	}
}
