using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        public string GetCurrentUserName()
        {
            return "Admin";
        }
    }
}