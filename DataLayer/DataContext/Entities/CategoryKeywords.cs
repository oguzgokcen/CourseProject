using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class CategoryKeywords
	{
		[Key]
		public int Id { get; set; }
		public string Keyword { get; set; } = default!;
		public string SearchTerm { get; set; } = default!;
		public List<Course> Courses { get; set; } = [];
	}
}
