namespace InfrastructureManagement.Core.Exceptions;

/// <summary>
/// hieunv
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string message)
        : base("Not Found", message)
    {
    }
}