using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class CarouselItem
	{
		public int Id { get; set; }
		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string ImageUrl { get; set; } = default!;
	}
}
