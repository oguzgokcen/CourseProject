using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.Cart
{
	public record GetCartItemsDto
	{
		public IEnumerable<CourseDetailDto> Courses { get; init; }
		public decimal TotalPrice { get; init; }
	}

}
