using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BudgetTracker.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BudgetTracker.API.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid Id
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        }
    }

    public List<string>? Roles =>
        _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
}
