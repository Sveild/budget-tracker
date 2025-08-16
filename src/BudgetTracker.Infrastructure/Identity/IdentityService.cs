using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.Application.Common.Interfaces;
using BudgetTracker.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetTracker.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILookupNormalizer _keyNormalizer;
    private readonly IConfiguration _configuration;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILookupNormalizer keyNormalizer,
        IConfiguration configuration,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _keyNormalizer = keyNormalizer;
        _configuration = configuration;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    private async Task<ApplicationUser?> GetUserAsync(Guid userId)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        return (await GetUserAsync(userId))?.UserName;
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = await GetUserAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        var user = await GetUserAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<string> SignInAsync(string usernameOrEmail, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u =>
            u.NormalizedUserName == _keyNormalizer.NormalizeName(usernameOrEmail)
            || u.NormalizedEmail == _keyNormalizer.NormalizeEmail(usernameOrEmail)
        );

        if (user == default(ApplicationUser))
        {
            return string.Empty;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
        {
            return string.Empty;
        }

        var claims = new[] { new Claim(ClaimTypes.Name, user.UserName!), new Claim(ClaimTypes.Email, user.Email!) };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string email, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u =>
            u.NormalizedEmail == _keyNormalizer.NormalizeEmail(email)
        );

        if (user != default(ApplicationUser))
        {
            return (Result.Failure([$"Email '{email}' is already taken."]), Guid.Empty);
        }

        user = new ApplicationUser { UserName = userName, Email = email };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await GetUserAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}
