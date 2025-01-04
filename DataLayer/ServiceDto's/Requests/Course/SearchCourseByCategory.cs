using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Enums;

namespace CourseApi.DataLayer.ServiceDto_s.Requests.Course
{
	public record SearchCourseByCategory
(string SearchTerm,
	double? MinRating,
	int? PageSize,
	int? PageNumber,
	Language? Language
	);
}
