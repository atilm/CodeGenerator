using System.Collections.Generic;
using System.Reflection;
using CodeGenerator.Entities;
using CodeGenerator.Interfaces;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Services
{
    /// <summary>
    /// Reads all defined classes and their properties in the dll of the project "Models"
    /// into a list of ClassDefinitions.
    /// </summary>
    public class DomainModelReader : IDomainModelReader
    {
        private readonly Assembly m_assembly;
        private ITypeToStringConverter typeToStringConverter;

        public DomainModelReader(IOptions<AppSettings> config, ITypeToStringConverter typeToStringConverter)
        {
            m_assembly = Assembly.LoadFile(AppSettings.ToFullPath(config.Value.DomainModelDllPath));

            this.typeToStringConverter = typeToStringConverter;
        }

        public IList<ClassDefinition> Read()
        {
            var types = m_assembly.GetTypes();

            var classDefinitions = new List<ClassDefinition>();

            foreach (var type in types)
            {
                var classDefinition = new ClassDefinition {Name = type.Name};

                var properties = type.GetProperties();

                foreach (var property in properties)
                {
                    var propertyTypeString = typeToStringConverter.GetString(property.PropertyType);

                    classDefinition.AddPropertyDefinition(
                        property.Name, 
                        propertyTypeString);
                }

                classDefinitions.Add(classDefinition);
            }

            return classDefinitions;
        }
    }
}