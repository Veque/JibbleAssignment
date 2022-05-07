using System.Net;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ODataConsoleApp.Exceptions;

namespace ODataConsoleApp.Implementations;

public abstract class HttpClientBase
{
    private HttpClient _client;
    private string _address;
    private JsonMediaTypeFormatter _formatter;

    protected HttpClientBase(string address, int? timeoutMs)
    {
        _client = new HttpClient();
        _client.Timeout = TimeSpan.FromMilliseconds(timeoutMs ?? 5000);
        _address = address;

        _formatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>(new[]
                {
                    new StringEnumConverter()
                }),
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                }
            }
        };
    }

    protected async Task<TResponse> Get<TResponse>(string path, CancellationToken token) where TResponse : class
    {
        var response = await _client.GetAsync(Path.Combine(_address, path));
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        await ThrowIfError(response, token);
        return await response.Content.ReadAsAsync<TResponse>(new[] { _formatter }, token);
    }

    protected async Task Post<TRequest>(string path, TRequest request, CancellationToken token)
    {
        var response = await _client.PostAsync(Path.Combine(_address, path), request, _formatter, token);
        await ThrowIfError(response, token);
    }

    protected async Task Delete(string path, CancellationToken token)
    {
        var response = await _client.DeleteAsync(Path.Combine(_address, path), token);
        await ThrowIfError(response, token);
    }

    private async Task ThrowIfError(HttpResponseMessage response, CancellationToken token)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var message = await response.Content.ReadAsStringAsync(token);
        throw new HttpException((int)response.StatusCode, message);
    }
}