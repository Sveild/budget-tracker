using System.Threading.Tasks;
using BudgetTracker.Application.Authentication.Commands.LogIn;
using BudgetTracker.Application.Authentication.Commands.Register;
using BudgetTracker.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.API.Controllers;

public class AuthenticationController : BaseSenderController
{
    public AuthenticationController(ISender sender)
        : base(sender) { }

    [HttpPost("[action]")]
    public async Task<ActionResult<Result>> Register([FromBody] RegisterCommand command)
    {
        return await _sender.Send(command);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<LogInResult>> LogIn([FromBody] LogInCommand command)
    {
        return await _sender.Send(command);
    }
}
