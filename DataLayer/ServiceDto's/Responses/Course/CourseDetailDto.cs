using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.Course
{
	public record CourseDetailDto
	{
		public int Id { get; init; }
		public string Title { get; init; }
		public string DescriptionHeader { get; init; }
		public DateTime PublishedDate { get; init; }
		public decimal Price { get; init; }
		public string Language { get; init; }
		public double Rating { get; init; }
		public int NumberOfRatings { get; init; }
		public int NumberOfStudents { get; init; }
		public string Description { get; init; }
		public string ImageUrl { get; init; }
		public List<CategoryDto> Categories { get; init; }
		public string InstructorId { get; init; }
		public InstructorDto Instructor { get; init; }
	}

	public record CategoryDto
	{
		public string Keyword { get; init; }
		public string SearchTerm { get; init; }
	}

	public record InstructorDto
	{
		public string FullName { get; init; }
		public string Email { get; init; }
		public int StudentCount { get; set; }
		public int CourseCount { get; set; }
		public string Description { get; set; }
	}
}
