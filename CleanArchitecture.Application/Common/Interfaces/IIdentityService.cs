using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Contracts.Request;
using CleanArchitecture.Application.Common.Contracts.Response;

namespace CleanArchitecture.Application.Common.Interfaces
{
   public interface IIdentityService
   {
      Task<UserResponse> Login(LoginUserRequest request);
      Task<UserResponse> Register(RegisterUserRequest request);
      Task<UserResponse> GetCurrentUser();
   }
}