using System;

namespace CodeGenerator.Interfaces
{
    public interface ITypeToStringConverter
    {
        string GetString(Type type);
    }
}