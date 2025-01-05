using CourseApi.Service.Services.HomeManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
	[AllowAnonymous]
    public class HomeController(IHomeService _homeService) : BaseApiController
    {
		[HttpGet("CarouselItems")]
		public async Task<IActionResult> Index()
		{
			var result = await _homeService.GetCarouselItems();
			return ActionResultInstance(result);
		}
	}
}
