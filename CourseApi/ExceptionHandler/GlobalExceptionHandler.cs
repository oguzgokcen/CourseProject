using CourseApi.DataLayer.ServiceDto_s.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.ExceptionHandler
{
	public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError(exception , "An unhandled exception occurred.");

			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			httpContext.Response.ContentType = "application/json";

			await httpContext.Response.WriteAsJsonAsync(BaseApiResponse<bool>.Error("Internal Server Error",500),cancellationToken);

			return true;
		}
	}
}
