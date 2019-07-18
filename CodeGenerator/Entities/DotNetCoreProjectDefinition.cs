using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeGenerator.Entities
{
    public enum DotNetCoreProjectType
    {
        AngularApp,
        Dll
    }

    public class DotNetCoreProjectDefinition
    {
        public string ProjectName { get; set; }
        public string ProjectFilePath { get; set; }
        public DotNetCoreProjectType ProjectType { get; set; }

        public IList<DotNetCoreProjectDefinition> References = new List<DotNetCoreProjectDefinition>();

        public string TypeString
        {
            get
            {
                switch (ProjectType)
                {
                    case DotNetCoreProjectType.AngularApp:
                        return "angular";
                    case DotNetCoreProjectType.Dll:
                        return "classlib";
                    default:
                        throw new InvalidOperationException("Unhandled dotnet cli project type.");
                }
            }
        }
    }
}