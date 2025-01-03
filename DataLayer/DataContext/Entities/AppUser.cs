using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class AppUser:IdentityUser<Guid>
	{
		public string FullName { get; set; } = default!;
		public List<Course> BoughtCourses { get; set; } = [];
		public List<Course> CreatedCourses { get; set; } = [];
		public List<CartItem> CartItems { get; set; } = [];
		public List<PaymentLog> PaymentLogs { get; set; } = [];
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public string Website { get; set; } = "";
	}
}
