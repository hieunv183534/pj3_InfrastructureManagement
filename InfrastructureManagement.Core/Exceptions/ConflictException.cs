namespace InfrastructureManagement.Core.Exceptions;

/// <summary>
///hieunv
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ConflictException : ApplicationException
{
    public ConflictException(string message)
        : base("Conflict", message)
    {
    }
}