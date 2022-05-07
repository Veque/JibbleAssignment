using ODataConsoleApp.Interfaces;
using ODataConsoleApp.Models;

namespace ODataConsoleApp.Implementations;

public class ConsolePeopleService:IConsolePeopleService
{
    private ITrippinClient _client;

    public ConsolePeopleService(ITrippinClient client)
    {
        _client = client;
    }

    public async Task Query(string firstName, string lastName, CancellationToken token)
    {
        var result = await _client.QueryPeople(new PeopleQuery
        {
            FirstName = firstName,
            LastName = lastName
        }, token);

        var output = result.value.Select(x => $"{x.UserName}\t{x.FirstName}\t{x.LastName}").ToArray();
        
        Console.WriteLine("User Name\tFirst Name\tLast Name");
        Console.WriteLine(string.Join("\r\n", output));
    }

    public async Task GetDetails(string userName, CancellationToken token)
    {
        var result = await _client.GetDetails(userName, token);
        
        Console.WriteLine($"User Name: {result.UserName}, First Name: {result.FirstName}, Last Name: {result.LastName}");
    }

    public async Task Create(CancellationToken token)
    {
        Console.Write("User Name: ");
        var userName = Console.ReadLine();
        
        Console.Write("First Name: ");
        var firstName = Console.ReadLine();
        
        Console.Write("Last Name: ");
        var lastName = Console.ReadLine();

        await _client.CreatePeople(new PeopleItem
        {
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
        }, token);
    }

    public Task Delete(string userName, CancellationToken token)
    {
        return _client.DeletePeople(userName, token);
    }
}