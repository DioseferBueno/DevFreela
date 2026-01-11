using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project?> GetDetailsById(int id);
        Task<int> Add(Project project);
        Task Update(Project project);
        Task AddComment(ProjectComment comment);
        Task<bool> Exists(int id);
    }
}
