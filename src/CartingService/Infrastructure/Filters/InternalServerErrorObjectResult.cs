using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Infrastructure.Filters;


public class InternalServerErrorObjectResult : ObjectResult
{
	public InternalServerErrorObjectResult(object error)
		: base(error)
	{
		StatusCode = StatusCodes.Status500InternalServerError;
	}
}
