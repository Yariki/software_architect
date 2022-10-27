using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    private LinkGenerator _linkGenerator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    
    protected LinkGenerator LinkGenerator => _linkGenerator ??= HttpContext.RequestServices.GetRequiredService<LinkGenerator>();
}
