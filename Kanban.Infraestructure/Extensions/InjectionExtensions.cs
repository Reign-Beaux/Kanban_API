using Kanban.Infraestructure.UnitsOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infraestructure.Extensions
{
  public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      return services;
    }
  }
}
