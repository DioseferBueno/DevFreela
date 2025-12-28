using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Skills.InsertSkills
{
    public class InsertSkillsCommand : IRequest<ResultViewModel<int>>
    {
        public string Description { get; set; }

        public DevFreela.Core.Entities.Skill ToEntity()
        => new DevFreela.Core.Entities.Skill(Description);
    }
}
