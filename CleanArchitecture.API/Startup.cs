using System;
using System.Text;
using CleanArchitecture.API.Middleware;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.API
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddApplication();
         services.AddInfrastructure(Configuration);
         // Adding cors policy
         services.AddCors(opt =>
            {
               opt.AddPolicy(GlobalConstants.CORS_POLICY, policy =>
                  {
                     policy.AllowAnyHeader().AllowAnyMethod()
                           // Validate unathorize when token expires for 401 error
                           .WithExposedHeaders(GlobalConstants.WWW_Authenticate)
                           .WithOrigins(GlobalConstants.LOCAL_HOST_DOMAIN);
                  });
            });

         services.AddControllers(opt =>
         {
            // Add Authorization Policy for endpoints
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
         });

         // Policies to handle Roles
         services.AddAuthorization(opt =>
         {
            opt.AddPolicy(GlobalConstants.REQUIRE_ADMIN_ROLE, policy => policy.RequireRole(GlobalConstants.ADMIN_ROLE));
            opt.AddPolicy(GlobalConstants.REQUIRE_MODERATOR_ROLE, policy => policy.RequireRole(GlobalConstants.ADMIN_ROLE, GlobalConstants.MODERATOR_ROLE));
         });

         // Add JwtBearerToken configuration to use with appSettings
         var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Configuration[GlobalConstants.TOKEN_KEY_SECTION]));
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
               opt.TokenValidationParameters = new TokenValidationParameters
               {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = key,
                  ValidateAudience = false,
                  ValidateIssuer = false,
                  // To Handler token exp
                  ValidateLifetime = true,
                  ClockSkew = TimeSpan.Zero
               };
            });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseMiddleware<ErrorHandlingMiddleware>();

         // app.UseHttpsRedirection();

         app.UseRouting();

         app.UseCors(GlobalConstants.CORS_POLICY);

         // For use Authentication add this service
         app.UseAuthentication();
         // For use Authorization
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
