namespace InfrastructureManagement.Core.Exceptions;

/// <summary>
/// hieunv
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class FailureSaveChangesException : ApplicationException
{
    public FailureSaveChangesException(string message)
        : base("Bad Request", message)
    {
    }
}