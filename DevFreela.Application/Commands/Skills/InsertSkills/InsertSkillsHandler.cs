using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.Skills.InsertSkills
{
    public class InsertSkillsHandler : IRequestHandler<InsertSkillsCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;

        public InsertSkillsHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillsCommand request, CancellationToken cancellationToken)
        {
            if (request.Description == null) 
                return await Task.FromResult(ResultViewModel<int>.Fail("Description is required"));
            
            var skills = request.ToEntity();
            await _context.Skills.AddAsync(skills);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(skills.Id);
        }
    }
}
