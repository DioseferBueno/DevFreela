using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersConttroller : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        public UsersConttroller(DevFreelaDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(x => x.Skills)
                    .ThenInclude(u => u.Skill)
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var model = UserViewModel.FromEntity(user);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = new DevFreela.Core.Entities.User(model.FullName, model.Email, model.BirthDate);
            _context.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PosttSkills(int id, UserSkillsInputModel model)
        {
            var userSkills = model.SkillsId.Select(s => new UserSkill(id, s)).ToList();

            _context.AddRange(userSkills);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostCover(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
    }
}
