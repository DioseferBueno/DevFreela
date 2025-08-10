using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class ProjectItemViewModel
    {
        public ProjectItemViewModel(int id, string title, string clientName, string freelancerName, decimal totalCost)
        {
            Id = id;
            Title = title;
            ClientName = clientName;
            FreelancerName = freelancerName;
            TotalCost = totalCost;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string FreelancerName { get; set; }
        public decimal TotalCost { get; set; }

        public static ProjectItemViewModel FromEntity(Project project)
            => new(project.Id, project.Title, project.Client.FullName, project.Freelancer.FullName, project.TotalCost);
    }
}
