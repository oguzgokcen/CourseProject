using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class InstructorDetail
	{
		public Guid InstructorId { get; set; }
		public AppUser InstructorUser { get; set; }
		public List<Course> CreatedCourses { get; set; } = [];
		public int StudentCount { get; set; }
		public int CourseCount { get; set; }
		public string Description { get; set; }
	}
}
