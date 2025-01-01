using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.Course
{
	public class GetCourseListDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string DescriptionHeader { get; set; }
		public decimal Price { get; set; }
		public double Rating { get; set; }
		public int NumberOfRatings { get; set; }
		public string ImageUrl { get; set; }
		public string InstructorId { get; set; }
		public InstructorDto Instructor { get; set; }
	}
}
