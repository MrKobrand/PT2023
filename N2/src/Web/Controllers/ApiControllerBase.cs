using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure;

namespace Web.Controllers;

[ApiController]
[KebabCaseNaming]
[ApiExceptionFilter]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
