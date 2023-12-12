using Kanban.Infraestructure.Kanban.UnitsOfWork;
using Kanban.Infraestructure.KanbanExtras.UnitsOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Infraestructure.Extensions
{
  public static class InjectionExtensions
  {
    public static IServiceCollection AddInjectionInfraestructure(this IServiceCollection services)
    {
      services.AddTransient<IUnitOfWorkKanban, UnitOfWorkKanban>();
      services.AddTransient<IUnitOfWorkKanbanExtras, UnitOfWorkKanbanExtras>();
      return services;
    }
  }
}
