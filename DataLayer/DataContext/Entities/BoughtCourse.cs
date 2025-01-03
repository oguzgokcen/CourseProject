using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class BoughtCourse
	{
		public Guid UserId { get; set; }
		public AppUser User { get; set; } = default!;
		public int CourseId { get; set; }
		public Course Course { get; set; } = default!;
		public DateTime? BoughtDate { get; set; }
		public Guid? PaymentLogId { get; set; }
		public PaymentLog? PaymentLog { get; set; }
	}
}
