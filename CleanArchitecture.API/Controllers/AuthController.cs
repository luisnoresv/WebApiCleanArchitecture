using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Contracts.Request;
using CleanArchitecture.Application.Common.Contracts.Response;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
      private readonly IIdentityService _identityService;
      public AuthController(IIdentityService identityService)
      {
         _identityService = identityService;
      }

      [HttpGet]
      public async Task<ActionResult<UserResponse>> GetCurrentUser()
      {
         return await _identityService.GetCurrentUser();
      }

      [AllowAnonymous]
      [HttpPost("login")]
      public async Task<ActionResult<UserResponse>> Login([FromBody]LoginUserRequest request)
      {
         return await _identityService.Login(request);
      }

      [AllowAnonymous]
      [HttpPost("register")]
      public async Task<ActionResult<UserResponse>> Register([FromBody]RegisterUserRequest request)
      {
         return await _identityService.Register(request);
      }
   }
}