namespace ODataConsoleApp.Interfaces;

public interface IConsolePeopleService
{
    Task Query(string firstName, string lastName, CancellationToken token);

    Task GetDetails(string userName, CancellationToken token);

    Task Create(CancellationToken token);

    Task Delete(string userName, CancellationToken token);
}