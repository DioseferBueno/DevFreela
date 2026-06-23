using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
        public string Search { get; }
        public int Page { get; }
        public int Size { get; }

        public GetAllProjectsQuery(string search = "", int page = 0, int size = 3)
        {
            Search = search ?? string.Empty;
            Page = page;
            Size = size;
        }
    }
}
