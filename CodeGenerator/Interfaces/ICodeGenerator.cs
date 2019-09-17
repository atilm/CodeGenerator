using System.Collections.Generic;
using CodeGenerator.Entities;

namespace CodeGenerator.Interfaces
{
    public interface ICodeGenerator
    {
        void Generate(IEnumerable<ClassDefinition> classDefinitions);
    }
}