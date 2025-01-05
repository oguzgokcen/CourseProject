using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Messaging;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;

namespace CourseApi.DataLayer.Repositories
{
	public interface IPaymentRepository
	{
		Task CreatePaymentLog(PaymentLogDto paymentLogDto);
		Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistory(Guid userId);
	}
}
