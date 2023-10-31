using FluentValidation;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Services;
using Kanban.Application.Utils;
using Kanban.Application.Validators.Login;
using Kanban.Application.Validators.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Application.Extensions
{
  public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
      services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);

      services.AddScoped<ExceptionsLogger>();
      services.AddScoped<LoginValidator>();
      services.AddScoped<UserValidator>();

      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
