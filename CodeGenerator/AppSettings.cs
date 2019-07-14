using System.IO;

namespace CodeGenerator
{
    public class AppSettings
    {
        public string DomainModelDllPath { get; set; }
        public string TemplateFolderPath { get; set; }
        public string ViewModelTargetFolderPath { get; set; }
        public string RepositoryTargetFolderPath { get; set; }
        public string ServiceTargetFolderPath { get; set; }
        public string MapTargetFolderPath { get; set; }
        public string AppProjectPath { get; set; }

        public static string ToFullPath(string relativePath)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            return Path.Join(currentDirectory, relativePath);
        }
    }
}