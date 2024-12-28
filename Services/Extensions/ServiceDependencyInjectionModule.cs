using CourseApi.DataLayer.CommonModels;
using CourseApi.Service.CourseServices.TokenManager;
using CourseApi.Service.CourseServices.UserManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Extensions
{
	public static class ServiceDependencyInjectionModule
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			#region services
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();
			#endregion
			return services;

		}
	}
}
