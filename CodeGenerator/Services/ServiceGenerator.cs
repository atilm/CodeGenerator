using System.Collections.Generic;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class ServiceGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public ServiceGenerator(
            IOptions<AppSettings> config,
            ITemplateFileReader templateFileReader, 
            IProjectFileWriter projectFileWriter) : 
            base(templateFileReader, projectFileWriter)
        {
            TargetFolderPath = AppSettings.ToFullPath(config.Value.ServiceTargetFolderPath);
        }

        public void Generate(IEnumerable<ClassDefinition> classDefinitions)
        {
            var interfaceTemplate = TemplateReader.Read("IServiceTemplate.cs.txt");
            var classTemplate = TemplateReader.Read("ServiceTemplate.cs.txt");

            foreach (var classDefinition in classDefinitions)
            {
                var domainObjectName = classDefinition.Name;
                var className = domainObjectName + "Service";
                var interfaceName = "I" + className;

                interfaceTemplate.TypeName = interfaceName;
                interfaceTemplate.Set("[[DomainObjectName]]", domainObjectName);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "Services",
                    null,
                    interfaceTemplate);

                classTemplate.TypeName = className;
                classTemplate.Set("[[DomainObjectName]]", domainObjectName);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "Services",
                    null,
                    classTemplate);
            }
        }
    }
}
