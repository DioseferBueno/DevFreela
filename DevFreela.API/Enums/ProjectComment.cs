using DevFreela.API.Entities;
using System.Security.Principal;

namespace DevFreela.API.Enums
{
    public class ProjectComment : BaseEntity
    {
        protected ProjectComment() { }
        public ProjectComment(string content, int idProject, int idUser)
        {
            Content = content;
            IdProject = idProject;
            IdUser = idUser;
        }
        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public Project Project { get; set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }

    }
}
