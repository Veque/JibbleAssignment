namespace ODataConsoleApp.Configuration;

public record TrippinService
{
    public string Url { get; init; }
    
    public string InstanceKey { get; init; }
    
    public int? TimeoutMs { get; init; }
}