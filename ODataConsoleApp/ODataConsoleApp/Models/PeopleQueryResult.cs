namespace ODataConsoleApp.Models;

public record PeopleQueryResult
{
    public PeopleItem[] value { get; init; }
}