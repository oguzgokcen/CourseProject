using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Messaging;

namespace CourseApi.DataLayer.Repositories
{
	public interface IPaymentRepository
	{
		Task CreatePaymentLog(PaymentLogDto paymentLogDto);
	}
}
