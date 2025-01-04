using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;

namespace CourseApi.DataLayer.ServiceDto_s.Messaging
{
	public record PaymentLogDto(Guid UserId, decimal TotalPrice, PaymentStatus PaymentStatus, List<int> BoughtCourseIds);
}
