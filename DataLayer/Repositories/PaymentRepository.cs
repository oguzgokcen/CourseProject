using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Messaging;
using CourseApi.DataLayer.ServiceDto_s.Responses.User;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataLayer.Repositories
{
	public class PaymentRepository(CourseDbContext _dbContext, IMapper _mapper):IPaymentRepository
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

		public async Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistory(Guid userId)
		{
			var paymentLogs = await _dbContext.PaymentLog
				.Include(x => x.BoughtCourses)
				.ThenInclude(x => x.Course)
				.Where(x => x.UserId == userId)
				.ProjectTo<PaymentHistoryDto>(_mapper.ConfigurationProvider)
				.ToListAsync();
			return paymentLogs;
		}

	}
}
