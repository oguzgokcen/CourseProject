using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using CourseApi.DataLayer.ServiceDto_s.Requests.Login;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Services.UserManager
{
	public interface IUserService
	{
		Task<BaseApiResponse<TokenDto>> UserLogin(LoginRequest loginRequest);
		Task<BaseApiResponse<TokenDto>> UserRegister(RegisterRequest registerRequest);
		Task<BaseApiResponse<UserDetailDto>> GetUserProfileById(string id);
		Task<BaseApiResponse<string>> UpdateUserProfile(UpdateUserDetailDto userDetailDto, string userId);
		Task<BaseApiResponse<IEnumerable<GetCourseListDto>>> GetUserCourses(Guid userId);
		Task<BaseApiResponse<TokenDto>> RefreshAcessToken(RefreshTokenRequest refreshTokenRequest);
		Task<BaseApiResponse<IEnumerable<GetCourseListDto>>> GetTeachersCourses(Guid teacherId);
		Task<BaseApiResponse<IEnumerable<PaymentHistoryDto>>> GetPaymentHistory(Guid userId);
	}
}