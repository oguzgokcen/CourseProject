using CourseApi.DataLayer.ServiceDto_s.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	[Route("/api/v1/[controller]")]
	[ApiController]
	public class BaseApiController : ControllerBase
	{
		public IActionResult ActionResultInstance<T>(BaseApiResponse<T> response) where T : class
		{
			return new ObjectResult(response)
			{
				StatusCode = response.StatusCode
			};
		}
	}
}
