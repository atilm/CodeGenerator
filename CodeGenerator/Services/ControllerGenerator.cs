using System;
using System.Collections.Generic;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class ControllerGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public ControllerGenerator(
            IOptions<AppSettings> config,
            ITemplateFileReader templateReader,
            IProjectFileWriter fileWriter)
            : base(templateReader, fileWriter)
        {
            TargetFolderPath = config.Value.AppProjectPath;
        }

        public void Generate(IEnumerable<ClassDefinition> classDefinitions)
        {
            var classTemplate = TemplateReader.Read("ControllerTemplate.cs.txt");

            var addServiceCommands = new List<string>();

            foreach (var classDefinition in classDefinitions)
            {
                var domainObjectName = classDefinition.Name;
                var className = domainObjectName + "Controller";

                classTemplate.TypeName = className;
                classTemplate.Set("[[DomainObjectName]]", domainObjectName);

                ProjectFileWriter.WriteToProject(
                    TargetFolderPath,
                    "WebApi",
                    "Controllers",
                    classTemplate);

                AppendAddServiceCommands(addServiceCommands, classDefinition);
            }

            GenerateStartupFile(addServiceCommands);
        }

        private void GenerateStartupFile(List<string> addServiceCommands)
        {
            var startupTemplate = TemplateReader.Read("StartupTemplate.cs.txt");

            var startupLineSeparator = Environment.NewLine + "                ";

            var addServiceCommandString = string.Join(startupLineSeparator, addServiceCommands);

            startupTemplate.TypeName = "Startup";
            startupTemplate.Set("[[AddServiceCommands]]", addServiceCommandString);

            ProjectFileWriter.WriteToProject(
                TargetFolderPath,
                "WebApi",
                null,
                startupTemplate);
        }

        private void AppendAddServiceCommands(List<string> addServiceCommands, ClassDefinition classDefinition)
        {
            var domainObject = classDefinition.Name;

            addServiceCommands.Add($".AddScoped<I{domainObject}Map, {domainObject}Map>()");
            addServiceCommands.Add($".AddScoped<I{domainObject}Service, {domainObject}Service>()");
            addServiceCommands.Add($".AddSingleton<I{domainObject}Repository, {domainObject}Repository>()");
        }
    }
}
