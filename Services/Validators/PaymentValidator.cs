using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using FluentValidation;

namespace CourseApi.Service.Validators
{
	public class PaymentValidator : AbstractValidator<PaymentRequestDto>
	{
		public PaymentValidator()
		{
			RuleFor(payment => payment.CardName)
				.NotEmpty().WithMessage("Card name is required.")
				.MaximumLength(100).WithMessage("Card name cannot exceed 100 characters.");

			RuleFor(payment => payment.CardNumber)
				.CreditCard().WithMessage("Invalid card number.");

			RuleFor(payment => payment.ExpirationMonth)
				.InclusiveBetween(1, 12).WithMessage("Expiration month must be between 1 and 12.");

			RuleFor(payment => payment.ExpirationYear+2000)
				.GreaterThanOrEqualTo(DateTime.Now.Year)
				.WithMessage("Expiration year must be greater than or equal to the current year.");

			RuleFor(payment => payment.Cvv)
				.InclusiveBetween(100, 9999).WithMessage("CVV must be a 3 or 4 digit number.");

			RuleFor(payment => payment.TotalPrice)
				.GreaterThan(0).WithMessage("Total price must be greater than zero.");

			RuleFor(payment => new { payment.ExpirationMonth, payment.ExpirationYear })
				.Must(x => IsValidExpirationDate(x.ExpirationMonth, x.ExpirationYear))
				.WithMessage("The expiration date must be in the future.");
		}
		private bool IsValidExpirationDate(int month, int year)
		{
			var now = DateTime.Now;
			var expirationDate = new DateTime(year+2000, month, DateTime.DaysInMonth(year, month));
			return expirationDate > now;
		}
	}

}
