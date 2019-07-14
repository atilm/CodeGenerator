using System.IO;
using System.Reflection;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public class ProjectInitializer : IProjectInitializer
    {
        private string configFileSourcePath;
        private string configFileTargetPath;
        private readonly ILogger logger;
        private string templateFolderSourcePath;
        private string templatefolderTargetPath;

        public ProjectInitializer(ILogger logger)
        {
            this.logger = logger;
        }

        public void Run()
        {
            InitPaths();

            CopyConfigFile();
            CopyTemplates();
        }

        private void CopyConfigFile()
        {
            logger.LogInfo($"Copy: {configFileSourcePath} to {configFileTargetPath}");

            File.Copy(configFileSourcePath, configFileTargetPath);
        }

        private void CopyTemplates()
        {
            logger.LogInfo($"Copy: {templateFolderSourcePath} to {templatefolderTargetPath}");

            FileUtilities.DirectoryCopy(templateFolderSourcePath, templatefolderTargetPath, true);
        }

        private void InitPaths()
        {
            var appDir = FileUtilities.GetApplicationDirectory();
            var currentDir = Directory.GetCurrentDirectory();

            configFileSourcePath = Path.Join(appDir, "code-generator-config.json");
            configFileTargetPath = Path.Join(currentDir, "code-generator-config.json");

            templateFolderSourcePath = Path.Join(appDir, "CodeTemplates");
            templatefolderTargetPath = Path.Join(currentDir, "CodeTemplates");
        }
    }
}