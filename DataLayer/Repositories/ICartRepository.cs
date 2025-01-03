using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.Repositories
{
	public interface ICartRepository
	{
		Task<IEnumerable<CourseDetailDto>> GetCartCourses(Guid userId);
		Task<IEnumerable<Course>> GetCoursesOfUserCart(Guid userId);
		Task AddCartItem(CartItem CartItem);
		void UpdateCart(CartItem Cart);
		void RemoveCartItem(CartItem cartItem);
		void ClearUserCart(Guid userId);
		Task<CartItem?> IsCartItemExists(int courseId, Guid userId);
		Task<bool> IsCourseExistsInUserCart(int courseId, Guid userId);

	}
}
