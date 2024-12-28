using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.CourseServices.UserManager
{
	public interface IUserService
	{
		Task<BaseApiResponse<string>> UserLogin(LoginRequest loginRequest);

		Task<BaseApiResponse<string>> UserRegister(RegisterRequest registerRequest);
	}
}
