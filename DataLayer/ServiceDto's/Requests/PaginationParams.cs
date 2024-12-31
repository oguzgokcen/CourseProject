using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CourseApi.DataLayer.ServiceDto_s.Requests
{
	public abstract record PaginationParams(int? PageSize, int? PageNumber);
}
