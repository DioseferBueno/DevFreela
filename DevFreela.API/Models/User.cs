using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class User
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool Active { get; set; }

        public List<UserSkill> Skills { get; private set; }
    }
}
