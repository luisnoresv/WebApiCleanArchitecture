using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ApiController : ControllerBase
  {
    private IMediator _mediator;
    protected IMediator Mediator
        => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
  }
}