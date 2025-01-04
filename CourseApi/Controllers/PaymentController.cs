 using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.ServiceDto_s.Requests.Payment;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;
using CourseApi.Service.Services.PaymentManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Controllers
{
	[Authorize]
	public class PaymentController(IPaymentService _paymentService, CourseDbContext _dbContext) : BaseApiController
	{
		[HttpPost]
		public async Task<IActionResult> CreatePayment(PaymentRequestDto paymentDto)
		{
			var result = await _paymentService.CreatePaymentAsync(paymentDto,UserId!.Value);
			return ActionResultInstance(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetLogs()
		{
			var data = await _dbContext.BoughtCourses.Include(x => x.User).Include(x => x.Course).ToListAsync();
			return Ok(data);
		}
	}
}
