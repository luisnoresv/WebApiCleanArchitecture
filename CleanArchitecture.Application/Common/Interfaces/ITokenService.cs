using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces
{
   public interface ITokenService
   {
      Task<string> CreateToken(ApplicationUser user);
   }
}