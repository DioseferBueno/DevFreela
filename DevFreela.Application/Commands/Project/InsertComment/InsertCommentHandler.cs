using DevFreela.Application.Models;
using DevFreela.Core.Enums;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Project.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public InsertCommentHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.Exists(request.IdProject);

            if (!exists)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            var comments = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _repository.AddComment(comments);

            return ResultViewModel.Success();
        }
    }
}
