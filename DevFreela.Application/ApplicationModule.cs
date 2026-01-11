using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services)
        {
            services.AddHandlers();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddTransient<MediatR.IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();
            return services;
        }
    }
}
