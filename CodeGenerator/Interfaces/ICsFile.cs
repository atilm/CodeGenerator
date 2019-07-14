namespace CodeGenerator.Interfaces
{
    /// <summary>
    /// Represents a C# file
    /// </summary>
    public interface ICsFile
    {
        string FileName { get; }
        string Contents { get; }
    }
}