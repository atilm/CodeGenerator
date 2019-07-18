using System.Linq;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public class DotNetCoreSolutionCreator : IDotNetCoreSolutionCreator
    {
        private readonly IDotNetCli dotNet;

        public DotNetCoreSolutionCreator(IDotNetCli dotNet)
        {
            this.dotNet = dotNet;
        }

        public void Create(
            DotNetCoreSolutionDefinition solution)
        {
            dotNet.NewSolution(solution.Name);

            foreach (var project in solution.Projects)
                dotNet.NewProjectInSolution(project.TypeString, project.ProjectName, solution.FileName);

            foreach (var project in solution.Projects)
                dotNet.AddReferences(project.ProjectName, project.References.Select(r => r.ProjectName));
        }
    }
}