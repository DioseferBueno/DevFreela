using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string title, string descripttion, int idClient, int idFreelancer, string clientName, string freelancerName,
            decimal totalCost, List<ProjectComment> comments)
        {
            Id = id;
            Title = title;
            Descripttion = descripttion;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
            Comments = comments.Select(c => c.Content).ToList();
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Descripttion { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public string ClientName { get; private set; }
        public string FreelancerName { get; private set; }
        public decimal TotalCost{ get; private set; }
        public List<string> Comments { get; private set; }

        public static ProjectViewModel FromEntity(Project entity)
            => new (entity.Id, entity.Title, entity.Description, entity.IdClient, entity.IdFreelancer,
                entity.Client.FullName, entity.Freelancer.FullName, entity.TotalCost, entity.Comments);

    }
}
