namespace InfrastructureManagement.Core.Exceptions;

/// <summary>
/// hieunv
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class BadRequestException : ApplicationException
{
    public BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}