using System.Security.Claims;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Security
{
   public class CurrentUserService : ICurrentUserService
   {
      private readonly IHttpContextAccessor _httpContextAccessor;
      public CurrentUserService(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }
      public string GetCurrentUserName()
      {
         return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
      }
   }
}