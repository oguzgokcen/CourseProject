using CourseApi.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Enums;
using CourseApi.DataLayer.ServiceDto_s.Messaging;
using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.UoW;
using FluentValidation;
using MassTransit;

namespace CourseApi.Service.Services.PaymentManager
{
	public class PaymentService(ICartRepository _cartRepository,ICourseRepository _courseRepository, IPaymentRepository _paymentRepository, IValidator<PaymentRequestDto> _paymentValidator, IPublishEndpoint _publishEndpoint,IUnitOfWork _unitOfWork) : IPaymentService
	{
		public async Task<BaseApiResponse<bool>> CreatePaymentAsync(PaymentRequestDto paymentDto,Guid userId)
		{
			var isValid = await _paymentValidator.ValidateAsync(paymentDto);

			if (!isValid.IsValid)
			{
				return BaseApiResponse<bool>.Error(isValid.Errors.Select(x=>x.ErrorMessage).FirstOrDefault());
			}

			var cartCourses = await _cartRepository.GetCoursesOfUserCart(userId);

			if(!cartCourses.Any()){
				return BaseApiResponse<bool>.Error("There is no item in your cart.");
			}

			var totalPrice = cartCourses.Sum(x => x.Price);

			if (totalPrice != paymentDto.TotalPrice)
			{
				return BaseApiResponse<bool>.Error("Price is not match with requested payment amount. Please try again later.");
			}

			await _courseRepository.AddCoursesToUser(cartCourses, userId);

			_cartRepository.ClearUserCart(userId);

			await _unitOfWork.SaveChangesAsync();

			try
			{
				await _publishEndpoint.Publish(new PaymentLogDto(userId, totalPrice, PaymentStatus.Completed,
					cartCourses.Select(x => x.Id).ToList()));
			}catch(Exception ex)
			{
				Console.WriteLine("Hata" + ex.Message);
			}
			Console.WriteLine("Continue task");

			return BaseApiResponse<bool>.Success(true);
		}

	}
}
