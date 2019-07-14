using System;
using System.IO;
using CodeGenerator.Interfaces;
using CodeGenerator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator
{
    class Program
    {
        private static ServiceProvider m_serviceProvider;

        static void Main(string[] args)
        {
            m_serviceProvider = GetServiceCollection()
                .AddScoped<ICodeGenerator, ViewModelGenerator>()
                .AddScoped<ICodeGenerator, RepositoryGenerator>()
                .AddScoped<ICodeGenerator, ServiceGenerator>()
                .AddScoped<ICodeGenerator, MapGenerator>()
                .AddScoped<ICodeGenerator, ControllerGenerator>()
                .BuildServiceProvider();

            var modelReader = m_serviceProvider.GetService<IDomainModelReader>();
            var generators = m_serviceProvider.GetServices<ICodeGenerator>();

            var classDefinitions = modelReader.Read();

            foreach (var generator in generators)
                generator.Generate(classDefinitions);

            Console.WriteLine("Press any key:");
            Console.ReadKey();
        }

        private static IServiceCollection GetServiceCollection()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            return new ServiceCollection()
                .AddOptions()
                .Configure<AppSettings>(configuration.GetSection("Configuration"))
                .AddScoped<IDomainModelReader, DomainModelReader>()
                .AddScoped<ITemplateFileReader, TemplateFileReader>()
                .AddScoped<IProjectFileWriter, ProjectFileWriter>()
                .AddScoped<ITypeToStringConverter, TypeToStringConverter>();
        }
    }
}
