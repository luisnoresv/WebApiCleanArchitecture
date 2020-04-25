using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Security;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
   public static class InfrastructureServices
   {
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString(GlobalConstants.EF_CONNECTION_STRING),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

         // Identity Configuration
         var builder = services.AddIdentityCore<ApplicationUser>();
         // To Add Identity
         var identityBuilder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
         identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
         //  Add Login and Sign In Manager services
         identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();
         // Add Role Manager Service
         identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();

         services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
         services.AddScoped<ICurrentUserService, CurrentUserService>();
         services.AddScoped<IIdentityService, IdentityService>();
         services.AddScoped<ITokenService, TokenService>();

         return services;
      }
   }
}