using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DevFreela.Infrastructure.Auth;

namespace DevFreela.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UsersConttroller : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IAuthService _authorizationService;
        public UsersConttroller(DevFreelaDbContext context, IAuthService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
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
            return Ok(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(CreateUserInputModel model)
        {
            var hash = _authorizationService.ComputerHash(model.Password);
            var user = new DevFreela.Core.Entities.User(model.FullName, model.Email, model.BirthDate, hash, model.Role);
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

        [HttpPut("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel model)
        {
            var hash = _authorizationService.ComputerHash(model.Password);

            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == hash);

            if (user is null)
            {
                var error = ResultViewModel<LoginViewModel?>.Error("Login ou senha incorreto.");
                return BadRequest(error);
            }

            var token = _authorizationService.GenerateToken(user.Email, user.Role);

            var viewModel = new LoginViewModel(token);

            var result = ResultViewModel<LoginViewModel>.Success(viewModel);

            return Ok(result);
        }
    }
}
