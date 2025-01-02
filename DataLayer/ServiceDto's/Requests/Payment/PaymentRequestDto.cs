using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Requests.Payment
{
	public record PaymentRequestDto(
		string CardName,
		string CardNumber,
		int expirationMonth,
		int expirationYear,
		int CVV,
		decimal TotalPrice
	);
}
