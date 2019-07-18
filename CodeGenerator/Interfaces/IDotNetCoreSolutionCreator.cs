using CodeGenerator.Entities;

namespace CodeGenerator.Interfaces
{
    public interface IDotNetCoreSolutionCreator
    {
        void Create(
            DotNetCoreSolutionDefinition solution);
    }
}