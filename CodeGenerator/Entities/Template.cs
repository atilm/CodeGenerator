using System.Collections.Generic;
using CodeGenerator.Interfaces;

namespace CodeGenerator.Entities
{
    public class Template : ICsFile
    {
        private readonly Dictionary<string, string> m_replacments;
        private readonly string m_templateText;

        public Template(string templateText)
        {
            m_templateText = templateText;
            m_replacments = new Dictionary<string, string>();
        }

        public string TypeName { get; set; }
        public string FileName => TypeName + ".cs";
        public string Contents => GetText();

        public void Set(string placeholder, string content)
        {
            m_replacments[placeholder] = content;
        }

        public string GetText()
        {
            var resultString = m_templateText;

            foreach (var (key, value) in m_replacments)
                resultString = resultString.Replace(key, value);

            return resultString;
        }
    }
}