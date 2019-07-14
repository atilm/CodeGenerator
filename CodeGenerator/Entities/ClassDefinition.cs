using System.Collections.Generic;

namespace CodeGenerator.Entities
{
    public class ClassDefinition
    {
        public string Name { get; set; }
        public List<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();

        public void AddPropertyDefinition(string name, string type)
        {
            Properties.Add(new PropertyDefinition
            {
                Name = name,
                TypeString = type
            });
        }
    }
}