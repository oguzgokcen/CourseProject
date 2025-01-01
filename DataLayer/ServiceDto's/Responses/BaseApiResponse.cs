using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.ServiceDto_s.Responses
{
	public class BaseApiResponse<T>
	{
		public T Data { get; private set; }
		public int StatusCode { get; private set; }
		public bool IsSuccessful { get; private set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public ProblemDetails ProblemDetails { get; private set; }

		public static BaseApiResponse<T> Success(T data, int statusCode = StatusCodes.Status200OK)
		{
			return new BaseApiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
		}

		public static BaseApiResponse<T> Error(string error, int statusCode = StatusCodes.Status400BadRequest)
		{
			var problemDetails = new ProblemDetails
			{
				Status = statusCode,
				Title = ((HttpStatusCode)statusCode).ToString(),
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Extensions = { { "errors", new[] { error } } }
			};

			return new BaseApiResponse<T> { IsSuccessful = false , StatusCode = statusCode, ProblemDetails = problemDetails};
		}
	}
}
