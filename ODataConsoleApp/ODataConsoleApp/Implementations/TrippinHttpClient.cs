using Microsoft.Extensions.Options;
using ODataConsoleApp.Configuration;
using ODataConsoleApp.Interfaces;
using ODataConsoleApp.Models;

namespace ODataConsoleApp.Implementations;

public class TrippinHttpClient : HttpClientBase, ITrippinClient
{
    private string _key;

    public TrippinHttpClient(IOptions<TrippinService> options) : base(options.Value.Url, options.Value.TimeoutMs)
    {
        var key = options.Value.InstanceKey;
        if (!string.IsNullOrEmpty(key))
        {
            _key = key;
        }
    }
    
    public Task<PeopleQueryResult> QueryPeople(PeopleQuery query, CancellationToken token)
    {
        var path = "People";
        var filters = new List<string>();
        if (query.FirstName != null)
        {
            filters.Add($"FirstName eq '{query.FirstName}'");
        }
        if (query.LastName != null)
        {
            filters.Add($"LastName eq '{query.LastName}'");
        }

        if (filters.Any())
        {
            path += "?$filter=" + string.Join(" and ", filters);
        }
        return Get<PeopleQueryResult>(ApplyKey(path), token);
    }

    public Task<PeopleItem> GetDetails(string userName, CancellationToken token)
    {
        var path = $"People('{userName}')";
        return Get<PeopleItem>(ApplyKey(path), token);
    }

    public Task CreatePeople(PeopleItem people, CancellationToken token)
    {
        return Post(ApplyKey("People"), people, token);
    }

    public Task DeletePeople(string userName, CancellationToken token)
    {
        var path = $"People('{userName}')";
        return Delete(ApplyKey(path), token);
    }

    private string ApplyKey(string path) => _key == null ? path : $"{_key}/{path}";
}