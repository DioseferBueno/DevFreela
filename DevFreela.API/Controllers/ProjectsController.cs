using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.Services;

namespace DevFreela.Application.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IProjectService _projectService;
        public ProjectsController(DevFreelaDbContext context, IProjectService projectService)
        {
            _context = context;
            _projectService = projectService;
        }

        /// <summary>
        /// Busca todos os projetos ou filtra por título ou descrição
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(string search = "", int page = 0, int size = 3)
        {
            var result = _projectService.GetAll(search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _projectService.Insert(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var result = _projectService.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _projectService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var resullt =_projectService.Complete(id);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {
            var result = _projectService.InsertComment(id, model);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}
