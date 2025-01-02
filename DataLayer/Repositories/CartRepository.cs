using CourseApi.DataLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataLayer.Repositories
{
	public class CartRepository(CourseDbContext _dbContext,IMapper _mapper) : ICartRepository
	{
		public async Task<IEnumerable<CourseDetailDto>> GetCartCourses(Guid userId)
		{
			return await _dbContext.CartItems.Include(x=>x.Course).Where(x => x.UserId == userId).Select(x=>x.Course).ProjectTo<CourseDetailDto>(_mapper.ConfigurationProvider).ToListAsync();
		}
		public async Task AddCartItem(CartItem CartItem)
		{
			await _dbContext.CartItems.AddAsync(CartItem);
		}
		public void UpdateCart(CartItem Cart)
		{
			_dbContext.CartItems.Update(Cart);
		}

		public void RemoveCartItem(CartItem cartItem)
		{
			_dbContext.CartItems.Remove(cartItem);
		}
		//public async Task<Cart> DeleteCart(int id)
		//{
		//	var Cart = await _dbContext.Carts.FirstOrDefaultAsync(x => x.Id == id);
		//	_dbContext.Carts.Remove(Cart);
		//	await _dbContext.SaveChangesAsync();
		//	return Cart;
		//}

		public async Task<CartItem?> IsCartItemExists(int courseId, Guid userId)
		{
			return await _dbContext.CartItems.FirstOrDefaultAsync(x => x.CourseId == courseId && x.UserId == userId);
		}
		public async Task<bool> IsCourseExistsInUserCart(int courseId, Guid userId)
		{
			return await _dbContext.CartItems.AnyAsync(x => x.CourseId == courseId && x.UserId == userId);
		}

	}
}
