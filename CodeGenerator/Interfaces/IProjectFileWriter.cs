namespace CodeGenerator.Interfaces
{
    public interface IProjectFileWriter
    {
        void WriteToProject(string projectFolderPath, string projectName, string folderName, ICsFile fileToWrite);
    }
}