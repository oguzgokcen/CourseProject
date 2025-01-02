using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Cart;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.Service.UoW;

namespace CourseApi.Service.Services.CartManager
{
	public class CartService(IUnitOfWork _unitOfWork, ICartRepository _cartRepository) :ICartService
	{
		public async Task<BaseApiResponse<GetCartItemsDto>> GetCartItems(Guid userId)
		{
			var courses =await _cartRepository.GetCartCourses(userId);
			var response = new GetCartItemsDto()
			{
				Courses = courses,
				TotalPrice = courses.Sum(x => x.Price)
			};
			return BaseApiResponse<GetCartItemsDto>.Success(response);
		}

		public async Task<BaseApiResponse<bool>> AddToCartAsync(int courseId, Guid userId)
		{
			var isExists = await _cartRepository.IsCourseExistsInUserCart(courseId, userId);

			if (isExists)
				return BaseApiResponse<bool>.Error("This course is already in your cart!");
			var cartItem = new CartItem()
			{
				CourseId = courseId,
				UserId = userId
			};
			await _cartRepository.AddCartItem(cartItem);
			await _unitOfWork.SaveChangesAsync();
			return BaseApiResponse<bool>.Success(true);
		}

		public async Task<BaseApiResponse<bool>> RemoveFromCartAsync(int courseId, Guid userId)
		{
			var cartItem = await _cartRepository.IsCartItemExists(courseId, userId);
			if (cartItem == null)
				return BaseApiResponse<bool>.Error("This course is not in your cart!");
			_cartRepository.RemoveCartItem(cartItem);
			await _unitOfWork.SaveChangesAsync();
			return BaseApiResponse<bool>.Success(true);
		}

	}
}
