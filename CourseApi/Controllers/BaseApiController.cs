using System.Security.Claims;
using CourseApi.DataLayer.ServiceDto_s.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CourseApi.Controllers
{
	[Route("/api/v1/[controller]")]
	[ApiController]
	public class BaseApiController : ControllerBase
	{
		protected Guid? UserId
		{
			get
			{
				if (User.Identity?.IsAuthenticated == true)
				{
					var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
					return userId != null ? Guid.Parse(userId) : (Guid?)null;
				}

				return null;
			}
		}
		public IActionResult ActionResultInstance<T>(BaseApiResponse<T> response)
		{
			return new ObjectResult(response)
			{
				StatusCode = response.StatusCode
			};
		}
	}
}
