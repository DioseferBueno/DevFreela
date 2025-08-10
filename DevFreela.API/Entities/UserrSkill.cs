namespace DevFreela.API.Entities
{
    public class UserSkill
    {
        public UserSkill(int idUser, int idUSkill) : base()
        {
            IdUser = idUser;
            IdUSkill = idUSkill;
        }

        public int Id { get; set; }
        public int IdUser { get; private set; }
        public int IdUSkill { get; private set; }
        public User User { get; private set; }
        public Skill Skill { get; private set; }

    }
}
