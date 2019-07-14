using System;
using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class ViewModelGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public ViewModelGenerator(IOptions<AppSettings> config, ITemplateFileReader templateReader,
            IProjectFileWriter fileWriter)
        : base(templateReader, fileWriter)
        {
            TargetFolderPath = config.Value.ViewModelTargetFolderPath;
        }

        public void Generate(IEnumerable<ClassDefinition> classDefinitions)
        {
            var template = TemplateReader.Read("ViewModelTemplate.cs.txt");

            foreach (var classDefinition in classDefinitions)
            {
                var propertiesString = CreatePropertiesString(classDefinition.Properties);

                var viewModelClassName = classDefinition.Name + "ViewModel";

                template.TypeName = viewModelClassName;
                template.Set("[[ClassName]]", viewModelClassName);
                template.Set("[[Properties]]", propertiesString);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath, 
                    "ViewModels", 
                    null,
                    template);
            }
        }

        private static string CreatePropertiesString(IEnumerable<PropertyDefinition> propertyDefinitions)
        {
            var lineSeparator = Environment.NewLine + "        ";

            var lines = GetLines(propertyDefinitions);

            return string.Join(lineSeparator, lines);
        }

        private static IEnumerable<string> GetLines(IEnumerable<PropertyDefinition> propertyDefinitions)
        {
            return propertyDefinitions.Select(property => 
                $"public {property.TypeString} {property.Name} {{ get; set; }}");
        }
    }
}