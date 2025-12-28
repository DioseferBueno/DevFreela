using DevFreela.Application.Models;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.Project.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {

        private readonly DevFreelaDbContext _context;

        public InsertCommentHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == request.IdUser);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            var comments = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _context.ProjectComments.AddAsync(comments);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
