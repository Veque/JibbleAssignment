using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ODataConsoleApp.Configuration;
using ODataConsoleApp.Implementations;
using ODataConsoleApp.Interfaces;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            WriteHelp();
        }

        var configuration = BuildConfiguration();
        var serviceProvider = BuildServiceProvider(configuration);

        var service = serviceProvider.GetRequiredService<IConsolePeopleService>();

        var token = CancellationToken.None;

        await HandleRequest(service, args, token);
    }

    private static async Task HandleRequest(IConsolePeopleService service, string[] args, CancellationToken token)
    {
        switch (args[0])
        {
            case "query":
                string firstName = null;
                string lastName = null;
                for (int i = 1; i < args.Length; i++)
                {
                    var arg = args[i].Split('=');
                    switch (arg[0])
                    {
                        case "FirstName":
                            firstName = arg[1];
                            break;
                        case "LastName":
                            lastName = arg[1];
                            break;
                    }
                }

                await service.Query(firstName, lastName, token);
                break;
            case "details":
                await service.GetDetails(args[1], token);
                break;
            case "create":
                await service.Create(token);
                break;
            case "delete":
                await service.Delete(args[1], token);
                break;
        }
    }

    private static void WriteHelp()
    {
        Console.WriteLine("First argument should be one of these commands: 'query','details', 'create'");
        Console.WriteLine(
            "query - queries people. Supports filtering by first name and last name. Example: 'query FirstName=Scott LastName=Ketchum'..");
        Console.WriteLine(
            "details - gets details about a specific person. Second argument should be her User Name. Example: 'details scottketchum'.");
        Console.WriteLine(
            "create - asks a user for detailed information and creates a person. No additional arguments required. Example: 'create'");
        Console.WriteLine(
            "delete - deletes a person. Second argument should be her User Name. Example: 'delete scottketchum'.");
    }

    private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

    private static IServiceProvider BuildServiceProvider(IConfiguration configuration) =>
        new ServiceCollection()
            .Configure<TrippinService>(configuration.GetSection(nameof(TrippinService)))
            .AddTransient<ITrippinClient, TrippinHttpClient>()
            .AddTransient<IConsolePeopleService, ConsolePeopleService>()
            .BuildServiceProvider();
}