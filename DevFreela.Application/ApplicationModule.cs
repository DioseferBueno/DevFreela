using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
             services.AddScoped<IProjectService, ProjectService>();
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
