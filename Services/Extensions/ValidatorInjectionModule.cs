using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using CourseApi.Service.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Requests;

namespace CourseApi.Service.Extensions
{
	public static class ValidatorInjectionModule
	{
		public static IServiceCollection AddValidators(this IServiceCollection services)
		{
			services.AddScoped<IValidator<PaymentRequestDto>,PaymentValidator>();
			services.AddScoped<IValidator<RegisterRequest>,UserValidator>();
			return services;
		}
	}
}
