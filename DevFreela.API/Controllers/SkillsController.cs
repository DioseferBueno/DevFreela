using DevFreela.Application.Commands.Skills.InsertSkills;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.Project.Skills.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.Application.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DevFreelaDbContext _context;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSkillsQuery());
            if (result == null)
                return BadRequest("Não há dados a serem retornados");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillsCommand command) 
        {
            var result = await _mediator.Send(command);
            return NoContent();

        }
    }
}
