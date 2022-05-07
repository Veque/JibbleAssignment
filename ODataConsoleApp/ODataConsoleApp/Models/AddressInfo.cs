namespace ODataConsoleApp.Models;

public record AddressInfo
{
    public string Address { get; init; }
    
    public AddressCity City { get; init; }
}