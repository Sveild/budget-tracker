using System;
using Microsoft.AspNetCore.Identity;

namespace BudgetTracker.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole(string name)
        : base(name) { }
}
