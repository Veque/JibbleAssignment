namespace ODataConsoleApp.Models;

public record PeopleQuery
{
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
}