using System.IO;
using CodeGenerator.Interfaces;
using Microsoft.Build.Evaluation;

namespace CodeGenerator.Services
{
    public class ProjectFileWriter : IProjectFileWriter
    {
        public void WriteToProject(string projectFolderPath, string projectName, string folderName, ICsFile file)
        {
            var csprojFilePath = Path.Join(projectFolderPath, projectName + ".csproj");
            var filePath = Path.Join(projectFolderPath, folderName, file.FileName);

            var fileExistedBefore = File.Exists(filePath);

            File.WriteAllText(filePath, file.Contents);

            if (fileExistedBefore)
                return;

            AddFileToProject(csprojFilePath, filePath);
        }

        private static void AddFileToProject(string csprojFilePath, string filePath)
        {
            var project = new Project(csprojFilePath);
            project.AddItem("Compile", filePath);
            project.Save();
        }
    }
}