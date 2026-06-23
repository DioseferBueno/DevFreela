using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicaiton(this IServiceCollection services)
        {
            services
                .AddHandlers()
                .AddValidation();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());

            services.AddTransient<MediatR.IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();
            // Register other MediatR handlers in the Application assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllProjectsQuery>());
            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
            return services;
        }
    }
}
