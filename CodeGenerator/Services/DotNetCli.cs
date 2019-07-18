using System.Collections.Generic;
using System.Diagnostics;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public class DotNetCli : IDotNetCli
    {
        private const string DotNetCommand = "dotnet";
        private readonly ILogger logger;

        public DotNetCli(ILogger logger)
        {
            this.logger = logger;
        }

        public void NewSolution(string solutionName)
        {
            DotNet("sln", OutOption(solutionName));
        }

        public void NewProjectInSolution(
            string template,
            string projectName,
            string solutionFilePath)
        {
            var options = $"{template} {OutOption(projectName)}";

            DotNet("new", options);

            DotNet("sln", $"add {ProjectFileFromName(projectName)}");
        }

        public void AddReferences(string projectName, IEnumerable<string> projectNamesToReference)
        {
            foreach (var reference in projectNamesToReference)
                DotNet("add", $"{ProjectFileFromName(projectName)} reference {ProjectFileFromName(reference)}");
        }

        private void DotNet(string verb, string options)
        {
            var parameters = $"{verb} {options}";

            logger.LogInfo($"Executing: dotnet {verb} {options}");

            Process.Start(DotNetCommand, parameters);
        }

        private string OutOption(string name)
        {
            return $"-o {name}";
        }

        private string ProjectFileFromName(string name)
        {
            return $"{name}\\{name}.csproj";
        }
    }
}