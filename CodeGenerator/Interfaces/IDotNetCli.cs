using System.Collections.Generic;

namespace CodeGenerator.Interfaces
{
    public interface IDotNetCli
    {
        void NewSolution(string solutionName);

        void NewProjectInSolution(
            string template,
            string projectName,
            string solutionFilePath);

        void AddReferences(string projectFile, IEnumerable<string> filesToReference);
    }
}