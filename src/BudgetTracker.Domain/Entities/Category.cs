using System;
using BudgetTracker.Domain.Common;

namespace BudgetTracker.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = String.Empty;
    public string Icon { get; set; } = String.Empty;
}
