using System.Security.Claims;
using CourseApi.Service.Services.CartManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	[Authorize]
	public class CartController(ICartService _cartService) : BaseApiController
	{
		[HttpPost("{courseId}")]
		public async Task<IActionResult> AddToCart(int courseId)
		{
			var result = await _cartService.AddToCartAsync(courseId, UserId!.Value);
			return ActionResultInstance(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetCartCourses()
		{
			var result = await _cartService.GetCartItems(UserId!.Value);
			return ActionResultInstance(result);
		}


		[HttpDelete("{courseId}")]
		public async Task<IActionResult> RemoveFromCart(int courseId)
		{
			var result = await _cartService.RemoveFromCartAsync(courseId, UserId!.Value);
			return ActionResultInstance(result);
		}
	}
}
