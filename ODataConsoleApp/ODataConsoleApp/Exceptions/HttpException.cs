namespace ODataConsoleApp.Exceptions;

public class HttpException : Exception
{
    public int StatucCode { get; }

    public HttpException(int status, string message) : base($"Http error. Status: {status}. Message:{message}")
    {
        StatucCode = status;
    }
}