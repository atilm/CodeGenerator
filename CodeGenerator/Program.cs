using System;
using System.Collections.Generic;
using System.IO;
using CodeGenerator.Interfaces;
using CodeGenerator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommandLine;

namespace CodeGenerator
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

    class Program
    {
        private static ServiceProvider m_serviceProvider;
        private static ILogger logger;

        static void Main(string[] args)
        {
            ConfigureServices();

            logger = m_serviceProvider.GetService<ILogger>();

            Parser.Default.ParseArguments<InitOptions, GenerateOptions>(args)
                .WithParsed<InitOptions>(opts => InitializeProject())
                .WithParsed<GenerateOptions>(opts => RunGenerators());

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        private static void InitializeProject()
        {
            var initializer = m_serviceProvider.GetService<IProjectInitializer>();
            initializer.Run();
        }

        private static void RunGenerators()
        {
            logger.LogInfo("Generating code");

            var modelReader = m_serviceProvider.GetService<IDomainModelReader>();
            var generators = m_serviceProvider.GetServices<ICodeGenerator>();

            var classDefinitions = modelReader.Read();

            foreach (var generator in generators)
                generator.Generate(classDefinitions);
        }

        private static void ConfigureServices()
        {
            m_serviceProvider = GetServiceCollection()
                .AddTransient<ICodeGenerator, ViewModelGenerator>()
                .AddTransient<ICodeGenerator, RepositoryGenerator>()
                .AddTransient<ICodeGenerator, ServiceGenerator>()
                .AddTransient<ICodeGenerator, MapGenerator>()
                .AddTransient<ICodeGenerator, ControllerGenerator>()
                .BuildServiceProvider();
        }

        private static IServiceCollection GetServiceCollection()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("code-generator-config.json", true)
                .Build();

            return new ServiceCollection()
                .AddOptions()
                .Configure<AppSettings>(configuration.GetSection("Configuration"))
                .AddSingleton<ILogger, ConsoleLogger>()
                .AddTransient<IProjectInitializer, ProjectInitializer>()
                .AddTransient<IDomainModelReader, DomainModelReader>()
                .AddTransient<ITemplateFileReader, TemplateFileReader>()
                .AddTransient<IProjectFileWriter, ProjectFileWriter>()
                .AddTransient<ITypeToStringConverter, TypeToStringConverter>();
        }
    }
}
