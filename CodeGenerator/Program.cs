using CodeGenerator.Interfaces;
using CodeGenerator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ServiceProviderFactory.GetServiceProvider();

            var cli = serviceProvider.GetService<ICommandLineInterfaceRunner>();

            cli.Run(args);
        }
    }
}
