using System;
using System.Collections.Generic;

namespace BudgetTracker.Application.Common.Interfaces;

public interface IUser
{
    Guid Id { get; }
    List<string>? Roles { get; }
}
