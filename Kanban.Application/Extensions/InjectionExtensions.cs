using Kanban.Application.Services;
using Kanban.Domain.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Application.Extensions
{
  public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services)
    {
      services.AddScoped<IUserService, UserService>();

      return services;
    }
  }
}
