using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.API;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;

namespace CleanArchitecture.IntegrationTest
{

   [SetUpFixture]
   public class SetupTestEnvironment
   {
      private static IConfiguration _configuration;
      private static IServiceScopeFactory _scopeFactory;
      private static Checkpoint _checkPoint;
      private static string _currentUserName;

      [OneTimeSetUp]
      public void RunBeforeAnyTest()
      {
         var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(GlobalConstants.APP_SETTINGS_FILE, true, true)
            .AddEnvironmentVariables();

         _configuration = builder.Build();

         var services = new ServiceCollection();

         var startup = new Startup(_configuration);

         services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.ApplicationName == GlobalConstants.APPLICATION_NAME &&
            w.EnvironmentName == GlobalConstants.ENVIRONMENT_NAME));

         startup.ConfigureServices(services);

         // Replace service registration for ICurrentUserService
         // Remove existing registration
         var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));

         services.Remove(currentUserServiceDescriptor);

         var tokenServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ITokenService));

         services.Remove(tokenServiceDescriptor);
         // Register Testing Version
         var mockUser = new Mock<ApplicationUser>();

         services.AddTransient(provider =>
            Mock.Of<ICurrentUserService>(s => s.GetCurrentUserName() == _currentUserName));

         services.AddTransient(provider =>
                     Mock.Of<ITokenService>(s => s.CreateToken(mockUser.Object) == Task.FromResult(TestConstants.TOKEN)));

         _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

         _checkPoint = new Checkpoint
         {
            TablesToIgnore = new[] { GlobalConstants.MIGRATION_TABLE }
         };

         CreateDatabase();
      }


      private void CreateDatabase()
      {
         using var scope = _scopeFactory.CreateScope();

         var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

         context.Database.Migrate();
      }
      public static async Task ResetState()
      {
         await _checkPoint.Reset(_configuration.GetConnectionString(GlobalConstants.EF_CONNECTION_STRING));
         _currentUserName = null;
      }

      public static async Task<string> RunAsDefaultUserAsync()
      {
         return await RunAsUserAsync("admin", "P@ssw0rd");
      }

      private static async Task<string> RunAsUserAsync(string userName, string password)
      {
         using var scope = _scopeFactory.CreateScope();

         var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

         var user = new ApplicationUser { UserName = userName, Email = $"{userName}@test.com" };

         var result = await userManager.CreateAsync(user, password);

         _currentUserName = user.UserName;

         return _currentUserName;
      }

      public static async Task<TEntity> FindAsync<TEntity>(Guid id) where TEntity : class
      {
         using var scope = _scopeFactory.CreateScope();

         var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

         return await context.FindAsync<TEntity>(id);
      }

      public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
         where TEntity : class
      {
         using var scope = _scopeFactory.CreateScope();

         var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

         context.Add(entity);

         await context.SaveChangesAsync();

         return entity;
      }

      public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
      {
         using var scope = _scopeFactory.CreateScope();

         var mediator = scope.ServiceProvider.GetService<IMediator>();

         return await mediator.Send(request);
      }
   }
}