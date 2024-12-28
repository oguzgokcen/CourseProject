using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Requests
{
	public record RegisterRequest(string Email, string Password, string FullName);
}
