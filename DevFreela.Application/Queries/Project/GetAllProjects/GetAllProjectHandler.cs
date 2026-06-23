using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.Project.GetAllProjects
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly IProjectRepository _repository;

        public GetAllProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAllAsync();

            var filtered = projects
                .Where(p => string.IsNullOrEmpty(request.Search) || p.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase))
                .Select(ProjectItemViewModel.FromEntity)
                .ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(filtered);
        }
    }
}
