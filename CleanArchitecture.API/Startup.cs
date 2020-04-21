using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.API.Middleware;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Posts.Commands.CreatePost;
using CleanArchitecture.Application.Posts.Queries.GetPostDetail;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Security;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureProductionServices(IServiceCollection services)
      {
         services.AddDbContext<ApplicationDbContext>(opt =>
                     {
                        opt.UseSqlServer(Configuration.GetConnectionString(GlobalConstants.EF_CONNECTION_STRING));
                     });
         ConfigureServices(services);
      }

      public void ConfigureDevelopmentServices(IServiceCollection services)
      {
         services.AddDbContext<ApplicationDbContext>(opt =>
                     {
                        opt.UseSqlServer(Configuration.GetConnectionString(GlobalConstants.EF_CONNECTION_STRING));
                     });
         ConfigureServices(services);
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
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

         services.AddMediatR(typeof(GetPostDetailQueryHandler).Assembly);

         services.AddControllers(opt =>
         {
            // Add Authorization Policy for endpoints
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
         })
            // Adding FluentValidation
            .AddFluentValidation(cfg =>
            cfg.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>());

         // Identity Configuration
         var builder = services.AddIdentityCore<ApplicationUser>();
         // To Add Identity
         var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
         identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();

         services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
         services.AddScoped<ICurrentUserService, CurrentUserService>();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseMiddleware<ErrorHandlingMiddleware>();

         // app.UseHttpsRedirection();

         app.UseRouting();

         app.UseCors(GlobalConstants.CORS_POLICY);

         // Add this line
         app.UseAuthentication();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
