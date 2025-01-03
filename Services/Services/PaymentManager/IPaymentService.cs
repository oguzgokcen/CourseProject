using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Services.PaymentManager
{
	public interface IPaymentService
	{
		Task<BaseApiResponse<bool>> CreatePaymentAsync(PaymentRequestDto paymentDto, Guid userId);
	}
}
