using System;
using System.Collections.Generic;
using System.Text;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class MapGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public MapGenerator(
            IOptions<AppSettings> config,
            ITemplateFileReader templateReader,
            IProjectFileWriter fileWriter)
            : base(templateReader, fileWriter)
        {
            TargetFolderPath = config.Value.MapTargetFolderPath;
        }

        public void Generate(IEnumerable<ClassDefinition> classDefinitions)
        {
            var interfaceTemplate = TemplateReader.Read("IMapTemplate.cs.txt");
            var classTemplate = TemplateReader.Read("MapTemplate.cs.txt");

            foreach (var classDefinition in classDefinitions)
            {
                var domainObjectName = classDefinition.Name;
                var className = domainObjectName + "Map";
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
                classTemplate.Set(
                    "[[DomainToViewModelMappings]]", 
                    GenerateMappingString("domain", "viewModel", classDefinition.Properties));
                classTemplate.Set(
                    "[[ViewModelToDomainMappings]]", 
                    GenerateMappingString("viewModel", "domain", classDefinition.Properties));

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "Services",
                    null,
                    classTemplate);
            }
        }

        private string GenerateMappingString(
            string mappingSource, 
            string mappingTarget, 
            List<PropertyDefinition> propertyDefinitions)
        {
            var lineSeparator = Environment.NewLine + "            ";

            var lines = GetLines(mappingSource, mappingTarget, propertyDefinitions);

            return string.Join(lineSeparator, lines);
        }

        private IEnumerable<string> GetLines(
            string mappingSource,
            string mappingTarget,
            List<PropertyDefinition> propertyDefinitions)
        {
            foreach (var property in propertyDefinitions)
                yield return $"{mappingTarget}.{property.Name} = {mappingSource}.{property.Name};";
        }
    }
}
