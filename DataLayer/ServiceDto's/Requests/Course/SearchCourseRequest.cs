using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Requests.Course
{
	public record SearchCourseRequest(int? PageNumber, int? PageSize, string? Keyword = "");
}
