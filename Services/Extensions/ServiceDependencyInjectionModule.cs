using CourseApi.DataLayer.CommonModels;
using CourseApi.DataLayer.Repositories;
using CourseApi.Service.Services.CourseManager;
using CourseApi.Service.Services.TokenManager;
using CourseApi.Service.Services.UserManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.Service.Services.CartManager;
using CourseApi.Service.Services.PaymentManager;

namespace CourseApi.Service.Extensions
{
	public static class ServiceDependencyInjectionModule
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			#region repositories
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<ICartRepository,CartRepository>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			#endregion
			#region services
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<IPaymentService, PaymentService>();
			#endregion
			return services;

		}
	}
}
