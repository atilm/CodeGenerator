using System.IO;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Services
{
    public class ServiceProviderFactory
    {
        public static ServiceProvider GetServiceProvider()
        {
            return GetServiceCollection()
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
                .AddSingleton<ICommandLineInterfaceRunner, CommandLineInterfaceRunner>()
                .AddTransient<IProjectInitializer, ProjectInitializer>()
                .AddTransient<IDotNetCoreSolutionCreator, DotNetCoreSolutionCreator>()
                .AddTransient<IDotNetCli, DotNetCli>()
                .AddTransient<IDomainModelReader, DomainModelReader>()
                .AddTransient<ITemplateFileReader, TemplateFileReader>()
                .AddTransient<IProjectFileWriter, ProjectFileWriter>()
                .AddTransient<ITypeToStringConverter, TypeToStringConverter>();
        }
    }
}
