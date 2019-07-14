using System.Collections.Generic;
using CodeGenerator.Entities;

namespace CodeGenerator.Interfaces
{
    internal interface ICodeGenerator
    {
        void Generate(IEnumerable<ClassDefinition> classDefinitions);
    }
}