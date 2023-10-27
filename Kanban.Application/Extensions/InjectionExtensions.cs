using FluentValidation;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Services;
using Kanban.Application.Validators.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Application.Extensions
{
    public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
      services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);

      services.AddScoped<UserValidators>();

      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
