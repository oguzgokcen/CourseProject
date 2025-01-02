using CourseApi.DataLayer.ServiceDto_s.Responses;
using CourseApi.DataLayer.ServiceDto_s.Responses.Cart;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.Service.Services.CartManager
{
	public interface ICartService
	{
		Task<BaseApiResponse<GetCartItemsDto>> GetCartItems(Guid userId);
		Task<BaseApiResponse<bool>> AddToCartAsync(int courseId, Guid userId);
		Task<BaseApiResponse<bool>> RemoveFromCartAsync(int courseId, Guid userId);
	}
}
