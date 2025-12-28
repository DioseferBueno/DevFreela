using DevFreela.Application.Models;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _context;
        public ProjectService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            project.Complete();

            _context.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            project.SetAsDeleted();

            _context.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "")
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDDeleted && (search == "" || p.Title.Contains(search)));

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var project = _context.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .Include(p => p.Comments)
               .SingleOrDefault(p => p.Id == id && !p.IsDDeleted);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }
            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var project = model.ToEntity();
            _context.Add(project);
            _context.SaveChanges();
            return ResultViewModel<int>.Success(project.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            var comments = new ProjectComment(model.Content, model.IdProject, model.IdUser);

            _context.ProjectComments.Add(comments);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }

            project.Start();

            _context.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Update(UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == model.Id);
            if (project == null)
            {
                return ResultViewModel<ProjectViewModel>.Fail("Project not found");
            }
            project.Update(model.Title, model.Description, model.TotalCost);
            _context.Projects.Update(project);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
