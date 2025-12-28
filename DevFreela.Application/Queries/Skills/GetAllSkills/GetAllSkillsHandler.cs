using DevFreela.Application.Models;
using DevFreela.Application.Queries.Project.Skills.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.Skills.GetAllSkills
{
    public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery,List<Skill>>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllSkillsHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = _context.Skills.ToList();
            if (skills == null || skills.Count == 0)
            {
                return Task.FromException<List<Skill>>(new Exception("erro"));
            }

            return Task.FromResult(skills);
        }
    }
}
