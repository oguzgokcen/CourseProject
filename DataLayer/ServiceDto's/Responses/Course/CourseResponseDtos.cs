using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.Course
{
	public class CourseResponseDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string DescriptionHeader { get; set; }
		public DateTime PublishedDate { get; set; }
		public decimal Price { get; set; }
		public string Language { get; set; }
		public double Rating { get; set; }
		public int NumberOfRatings { get; set; }
		public int NumberOfStudents { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public List<CategoryDto> Categories { get; set; }
		public string InstructorId { get; set; }
		public InstructorDto Instructor { get; set; }
	}

	public class CategoryDto
	{
		public int Id { get; set; }
		public string Keyword { get; set; }
		public string SearchTerm { get; set; }
	}

	public class InstructorDto
	{
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
	}
}
