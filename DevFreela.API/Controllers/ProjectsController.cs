using DevFreela.Core.Enums;
using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly FreeLanceTotalCostConfig _config;

        private readonly DevFreelaDbContext _context;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll(string search = "", int page = 0, int size = 3) 
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDDeleted && (search == "" || p.Title.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();
            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p=> p.Comments)
                .SingleOrDefault(p => p.Id == id && !p.IsDDeleted);

            var model = ProjectViewModel.FromEntity(project);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        { 
            var project = model.ToEntity();
            _context.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById),new { id = 1}, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            project.Update(model.Title, model.Description, model.TotalCost);
            _context.Projects.Update(project);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.SetAsDeleted();

            _context.Update(project);
            _context.SaveChanges();


            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Start();
            
            _context.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Complete();

            _context.Update(project);
            _context.SaveChanges();


            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComments(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            var comments = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _context.ProjectComments.Add(comments);
            _context.SaveChanges();


            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }
    }
}
