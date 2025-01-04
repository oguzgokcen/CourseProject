using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.Course
{
	public record IsCourseBoughtDto(bool IsBought,bool isOnCart, DateTime? BoughtTime);
}
