using ODataConsoleApp.Models;

namespace ODataConsoleApp.Interfaces;

public interface ITrippinClient
{
    Task<PeopleQueryResult> QueryPeople(PeopleQuery query, CancellationToken token);

    Task<PeopleItem> GetDetails(string userName, CancellationToken token);

    Task CreatePeople(PeopleItem people, CancellationToken token);

    Task DeletePeople(string userName, CancellationToken token);
}