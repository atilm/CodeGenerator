using CodeGenerator.Entities;

namespace CodeGenerator.Interfaces
{
    public interface ITemplateFileReader
    {
        Template Read(string fileName);
    }
}