using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.InsertProject
{
    public class InsertProjectCommand : IRequest<ResultViewModel<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }

        public DevFreela.Core.Entities.Project ToEntity()
        => new DevFreela.Core.Entities.Project(Title, Description, IdClient, IdFreelancer, TotalCost);
    }
}
