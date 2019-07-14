using System;
using System.Collections.Generic;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Services
{
    public class TypeToStringConverter : ITypeToStringConverter
    {
        private static readonly Dictionary<Type, string> aliases =
            new Dictionary<Type, string>();

        static TypeToStringConverter()
        {
            aliases[typeof(byte)] = "byte";
            aliases[typeof(sbyte)] = "sbyte";
            aliases[typeof(short)] = "short";
            aliases[typeof(ushort)] = "ushort";
            aliases[typeof(int)] = "int";
            aliases[typeof(uint)] = "uint";
            aliases[typeof(long)] = "long";
            aliases[typeof(ulong)] = "ulong";
            aliases[typeof(char)] = "char";
            aliases[typeof(float)] = "float";
            aliases[typeof(double)] = "double";

            aliases[typeof(decimal)] = "decimal";

            aliases[typeof(bool)] = "bool";

            aliases[typeof(object)] = "object";
            aliases[typeof(string)] = "string";
        }

        public string GetString(Type type)
        {
            if (aliases.TryGetValue(type, out var value))
                return value;

            return type.ToString();
        }
    }
}