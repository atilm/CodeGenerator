using System.Collections.Generic;

namespace CodeGenerator.Entities
{
    public class DotNetCoreSolutionDefinition
    {
        public string Name { get; set; }

        public string FileName => $"{Name}.sln";

        public IList<DotNetCoreProjectDefinition> Projects = new List<DotNetCoreProjectDefinition>();
    }
}