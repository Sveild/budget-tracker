using System;
using System.Runtime.CompilerServices;
using BudgetTracker.Application.Common.Exceptions;

namespace BudgetTracker.Application.Common.RequestHandlers;

public abstract class BaseRequestHandler
{
    public static T GuardAgainstNull<T>(
        T? input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string? message = null
    )
    {
        if (input == null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName);
            }

            throw new ArgumentNullException(parameterName, message);
        }

        return input;
    }

    public static T GuardAgainstNotFound<TKey, T>(
        TKey key,
        T? input,
        [CallerArgumentExpression("input")] string? parameterName = null
    )
        where TKey : struct
    {
        GuardAgainstNull(key, nameof(key));

        if (input == null)
        {
            throw new NotFoundException(key.ToString()!, parameterName!);
        }

        return input;
    }
}
