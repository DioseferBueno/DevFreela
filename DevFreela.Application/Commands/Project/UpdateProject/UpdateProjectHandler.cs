using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace DevFreela.Application.Commands.Project.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;

        public UpdateProjectHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == request.Id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }
            project.Update(request.Title, request.Description, request.TotalCost);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
