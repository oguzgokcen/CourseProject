using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class CartItem
	{
		public int Id { get; set; }
		public int CourseId { get; set; } = default!;
		public Course Course { get; set; }
		public Guid UserId { get; set; } = default!;
		public AppUser User { get; set; }
	}
}
