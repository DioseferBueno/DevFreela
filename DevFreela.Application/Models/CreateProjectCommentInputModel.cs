
namespace DevFreela.Application.Models
{
    public class CreateProjectCommentInputModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }

    }
}
