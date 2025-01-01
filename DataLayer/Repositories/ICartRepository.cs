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
		Task AddCartItem(CartItem CartItem);

		Task<bool> IsCourseExistsInUserCart(int courseId, Guid userId);
	}
}
