using System.Collections.Generic;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class RepositoryGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public RepositoryGenerator(
            IOptions<AppSettings> config,
            ITemplateFileReader templateReader,
            IProjectFileWriter fileWriter)
            : base(templateReader, fileWriter)
        {
            TargetFolderPath = config.Value.RepositoryTargetFolderPath;
        }

        public void Generate(IEnumerable<ClassDefinition> classDefinitions)
        {
            var interfaceTemplate = TemplateReader.Read("IRepositoryTemplate.cs.txt");
            var classTemplate = TemplateReader.Read("RepositoryTemplate.cs.txt");

            foreach (var classDefinition in classDefinitions)
            {
                var domainObjectName = classDefinition.Name;
                var className = domainObjectName + "Repository";
                var interfaceName = "I" + className;

                interfaceTemplate.TypeName = interfaceName;
                interfaceTemplate.Set("[[InterfaceName]]", interfaceName);
                interfaceTemplate.Set("[[ClassName]]", className);
                interfaceTemplate.Set("[[DomainObjectName]]", domainObjectName);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "Repositories",
                    null,
                    interfaceTemplate);

                classTemplate.TypeName = className;
                classTemplate.Set("[[InterfaceName]]", interfaceName);
                classTemplate.Set("[[ClassName]]", className);
                classTemplate.Set("[[DomainObjectName]]", domainObjectName);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "Repositories",
                    null,
                    classTemplate);
            }
        }
    }
}