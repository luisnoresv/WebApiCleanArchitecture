using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Common.Behaviours
{
  public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
  {
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService,
       IIdentityService identityService)
    {
      _logger = logger;
      _currentUserService = currentUserService;
      _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
      var requestName = typeof(TRequest).Name;

      var currentUser = await _identityService.GetCurrentUser();

      var currentUserId = _currentUserService.GetCurrentUserId();

      _logger.LogInformation("CleanTesting Request: {Name} {@UserId} {@UserName} {@Request}",
         requestName, currentUserId, currentUser.UserName, request);
    }
  }
}