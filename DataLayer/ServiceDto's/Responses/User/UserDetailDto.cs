using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.User
{
	public record UserDetailDto (string FullName,
		string Email,string Title, string Website, string Description);

	public record UpdateUserDetailDto(string FullName,
		string Email, string Title, string Website, string Description);

}
