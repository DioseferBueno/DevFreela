using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services
{
    public interface ISkillService
    {
        ResultViewModel<List<Skill>> GetAll();
        ResultViewModel<int> Post(CreateSkillInputModel model);
    }

    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;

        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<Skill>> GetAll()
        {
            var result = _context.Skills.ToList();
            
            return ResultViewModel<List<Skill>>.Success(result);
        }

        public ResultViewModel<int> Post(CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
