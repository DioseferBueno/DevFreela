using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, string? password, string? role) : base()
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            Skills = [];
            OwnedProjects = [];
            FreelanceProjects = [];
            Comments = [];
            Password = password;
            Role = role;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Active { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }


    }
}
