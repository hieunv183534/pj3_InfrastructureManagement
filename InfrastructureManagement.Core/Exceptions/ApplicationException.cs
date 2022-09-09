using System;

namespace InfrastructureManagement.Core.Exceptions;

/// <summary>
/// hieunv
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public abstract class ApplicationException : Exception
{
    protected ApplicationException(string title, string message)
        : base(message) =>
        Title = title;

    public string Title { get; }
}