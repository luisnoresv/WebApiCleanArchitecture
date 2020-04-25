using System.Reflection;
using CleanArchitecture.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
   public static class ApplicationServices
   {
      public static IServiceCollection AddApplication(this IServiceCollection services)
      {
         services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
         services.AddMediatR(Assembly.GetExecutingAssembly());
         services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
         services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
         services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

         return services;
      }
   }
}