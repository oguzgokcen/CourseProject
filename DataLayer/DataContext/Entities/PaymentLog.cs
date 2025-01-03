using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Enums;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class PaymentLog
	{
		public Guid Id { get; set; }
		public Guid	UserId { get; set; }
		public AppUser User { get; set; }
		public DateTime CreatedOnUtc { get; set; }
		public decimal TotalPrice { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public List<BoughtCourse> BoughtCourses { get; set; } = [];
	}
}
