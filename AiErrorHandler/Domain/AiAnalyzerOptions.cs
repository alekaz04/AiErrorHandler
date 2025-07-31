namespace AiErrorHandler.Domain;

public class AiAnalyzerOptions
{
    public string Model { get; set; } = null!;
    public string Key { get; set; } = null!;

    public bool ProcessInBackground { get; set; } = true;

    public HashSet<string> IgnoredExceptions { get; set; } = new();

    public bool IsRussianLanguage { get; set; } = true;
    public bool IsWriteToLogger { get; set; }
}
