namespace ODataConsoleApp.Models;

public record PeopleItem
{
    public string UserName { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public Gender Gender { get; init; }
    
    public int? Age { get; init; }
    
    public AddressInfo[] AddressInfo { get; init; }
    
    public string[] Emails { get; init; }
}