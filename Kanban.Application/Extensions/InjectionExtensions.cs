using FluentValidation;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Application.Services;
using Kanban.Application.Validators.GroupProjects;
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
      services.AddScoped<EmailSender>();

      // Validators
      services.AddScoped<GroupProjectValidator>();
      services.AddScoped<LoginValidator>();
      services.AddScoped<UserValidator>();

      // Services
      services.AddScoped<IFeatureService, FeatureService>();
      services.AddScoped<IGroupProjectService, GroupProjectService>();
      services.AddScoped<ILoginService, LoginService>();
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
