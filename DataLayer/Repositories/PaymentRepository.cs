using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Migrations;
using CourseApi.DataLayer.ServiceDto_s.Messaging;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataLayer.Repositories
{
	public class PaymentRepository(CourseDbContext _dbContext):IPaymentRepository
	{

		public async Task CreatePaymentLog(PaymentLogDto paymentLogDto)
		{
			var boughtCourses = await _dbContext.BoughtCourses.Where(x =>
				x.UserId == paymentLogDto.UserId && paymentLogDto.BoughtCourseIds.Contains(x.CourseId)).ToListAsync();

			var paymentLog = new PaymentLog
			{
				UserId = paymentLogDto.UserId,
				TotalPrice = paymentLogDto.TotalPrice,
				PaymentStatus = paymentLogDto.PaymentStatus,
				CreatedOnUtc = DateTime.UtcNow,
				BoughtCourses = boughtCourses
			};
			await _dbContext.PaymentLog.AddAsync(paymentLog);
			foreach (var course in boughtCourses)
			{
				course.PaymentLog = paymentLog;
			}

			_dbContext.BoughtCourses.UpdateRange(boughtCourses);
		}
	}
}
