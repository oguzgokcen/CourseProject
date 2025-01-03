using CourseApi.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.Service.UoW;
using FluentValidation;

namespace CourseApi.Service.Services.PaymentManager
{
	public class PaymentService(ICartRepository _cartRepository,ICourseRepository _courseRepository, IValidator<PaymentRequestDto> _paymentValidator,IUnitOfWork _unitOfWork) : IPaymentService
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

			_cartRepository.ClearUserCart(userId);  //TODO:rabbit mq + logging

			await _unitOfWork.SaveChangesAsync();

			return BaseApiResponse<bool>.Success(true);
		}

	}
}
