using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Requests;
using FluentValidation;

namespace CourseApi.Service.Validators
{
	public class UserValidator : AbstractValidator<RegisterRequest>
	{
		public UserValidator()
		{
			RuleFor(u => u.FullName)
				.NotEmpty().WithMessage("Full name is required.");
			RuleFor(u => u.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("E mail adress is not right format");
			RuleFor(u => u.Password)
				.NotEmpty().WithMessage("Password is required");
		}
	}
}
