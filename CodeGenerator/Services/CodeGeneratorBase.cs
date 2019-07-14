using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public abstract class CodeGeneratorBase
    {
        protected CodeGeneratorBase(
            ITemplateFileReader templateReader,
            IProjectFileWriter fileWriter)
        {
            TemplateReader = templateReader;
            ProjectFileWriter = fileWriter;
        }

        protected IProjectFileWriter ProjectFileWriter { get; }
        protected ITemplateFileReader TemplateReader { get; }
        protected string TargetFolderPath { get; set; }
    }
}