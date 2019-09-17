using System.Collections.Generic;
using CodeGenerator.Interfaces;
using CommandLine;

namespace CodeGenerator.Services
{
    [Verb("init", HelpText = "Generate a code-generator-config.json file and a folder with default code templates in this directory.")]
    class InitOptions
    {
    }

    [Verb("generate", HelpText = "Generate files from class definitions given in the source dll.")]
    class GenerateOptions
    {
        [Option('f', "force", Required = false, HelpText = "Overwrite existing code files.")]
        public bool Force { get; set; }
    }

    public class CommandLineInterfaceRunner : ICommandLineInterfaceRunner
    {
        public CommandLineInterfaceRunner(
            ILogger logger,
            IProjectInitializer projectInitializer,
            IDomainModelReader domainModelReader,
            IEnumerable<ICodeGenerator> codeGenerators)
        {
            this.logger = logger;
            this.projectInitializer = projectInitializer;
            this.domainModelReader = domainModelReader;
            this.codeGenerators = codeGenerators;
        }

        public void Run(string[] commandLineArguments)
        {
            Parser.Default.ParseArguments<InitOptions, GenerateOptions>(commandLineArguments)
                .WithParsed<InitOptions>(opts => InitializeProject())
                .WithParsed<GenerateOptions>(opts => RunGenerators());
        }

        private void InitializeProject()
        {
            projectInitializer.Run();
        }

        private void RunGenerators()
        {
            logger.LogInfo("Generating code");

            var classDefinitions = domainModelReader.Read();

            foreach (var generator in codeGenerators)
                generator.Generate(classDefinitions);
        }

        private ILogger logger;
        private IProjectInitializer projectInitializer;
        private IDomainModelReader domainModelReader;
        private IEnumerable<ICodeGenerator> codeGenerators;
    }
}
