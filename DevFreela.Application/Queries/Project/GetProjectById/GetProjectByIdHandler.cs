using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Project.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;

        public GetProjectByIdHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Include(p => p.Comments)
               .SingleOrDefaultAsync(p => p.Id == request.Id && !p.IsDDeleted);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }
            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
