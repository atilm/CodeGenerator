using System.IO;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    public class TemplateFileReader : ITemplateFileReader
    {
        private string templateFolderPath;

        public TemplateFileReader(IOptions<AppSettings> config)
        {
            templateFolderPath = config.Value.TemplateFolderPath;
        }

        public Template Read(string fileName)
        {
            var filePath = Path.Join(templateFolderPath, fileName);

            var templateText = File.ReadAllText(filePath);

            return new Template(templateText);
        }
    }
}