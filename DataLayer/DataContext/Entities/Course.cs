using CourseApi.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class Course
	{
		public int Id { get; set; }
		public string Title { get; set; } = default!;
		public string DescriptionHeader { get; set; } = default!;
		public DateTime PublishedDate { get; set; }
		public decimal Price { get; set; }
		public Language Language { get; set; }
		public double Rating { get; set; }
		public int NumberOfRatings { get; set; }
		public int NumberOfStudents { get; set; }
		public string Description { get; set; } = default!;
		public string? ImageUrl { get; set; }
		public List<CategoryKeywords> Categories { get; set; } = [];
		public List<AppUser> Users { get; set; } = [];
		public List<CartItem> CartItems { get; set; } = [];
		public Guid InstructorId { get; set; }
		public InstructorDetail Instructor { get; set; } = default!;
	}
}
