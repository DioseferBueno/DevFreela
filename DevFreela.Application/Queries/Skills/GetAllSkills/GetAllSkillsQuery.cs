using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.Project.Skills.GetAllSkills
{
    public class GetAllSkillsQuery :IRequest<List<Skill>>
    {

    }
}
