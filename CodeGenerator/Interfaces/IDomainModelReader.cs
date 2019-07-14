using System.Collections.Generic;
using CodeGenerator.Entities;

namespace CodeGenerator.Interfaces
{
    public interface IDomainModelReader
    {
        IList<ClassDefinition> Read();
    }
}