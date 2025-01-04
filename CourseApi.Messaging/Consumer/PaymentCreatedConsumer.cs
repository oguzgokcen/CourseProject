using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Enums;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.ServiceDto_s.Messaging;
using CourseApi.DataLayer.UoW;
using MassTransit;

namespace CourseApi.Messaging.Consumer
{
	public class PaymentCreatedConsumer(ICartRepository _cartRepository, IPaymentRepository _paymentRepository,IUnitOfWork unitOfWork):IConsumer<PaymentLogDto>
	{
		public async Task Consume(ConsumeContext<PaymentLogDto> context)
		{
			var data = context.Message;

			await _paymentRepository.CreatePaymentLog(data);

			await unitOfWork.SaveChangesAsync();
			Console.WriteLine($"Payment created for user: {data.UserId}");
		}
	}
}
