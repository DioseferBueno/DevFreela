using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;


namespace DevFreela.Application.Commands.Project.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }
             project.Update(request.Title, request.Description, request.TotalCost);
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
