using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.API.Middleware;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Posts.Commands.CreatePost;
using CleanArchitecture.Application.Posts.Queries.GetPostDetail;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Security;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
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
                            // opt.UseLazyLoadingProxies();
                            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                        });
            ConfigureServices(services);
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                        {
                            // opt.UseLazyLoadingProxies();
                            opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                        });
            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding cors policy
            services.AddCors(opt =>
                {
                    opt.AddPolicy("CorsPolicy", policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod()
                            // Validate unathorize when token expires for 401 error
                            .WithExposedHeaders(GlobalConstants.WWW_Authenticate)
                            .WithOrigins("http://localhost:3000");
                    });
                });

            services.AddMediatR(typeof(GetPostDetailQueryHandler).Assembly);

            services.AddControllers()
                .AddFluentValidation(cfg =>
                 cfg.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>());

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
