using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.API
{
   public class Program
   {
      public async static Task Main(string[] args)
      {
         var host = CreateHostBuilder(args).Build();

         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            try
            {
               var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
               var context = services.GetRequiredService<ApplicationDbContext>();
               context.Database.Migrate();
               // Apply Seed
               await ApplicationDbContextSeed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {
               var logger = services.GetRequiredService<ILogger<Program>>();
               logger.LogError(ex, "An error ocurring during migration");
            }
         }

         await host.RunAsync();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
               webBuilder.UseStartup<Startup>();
            });
   }
}
