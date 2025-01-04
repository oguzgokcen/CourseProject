using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses
{
	public record PaginatedResult(Object Data, int TotalCount);
}
