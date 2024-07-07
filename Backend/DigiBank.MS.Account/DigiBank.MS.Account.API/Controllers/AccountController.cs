using Account_MS.Contract;
using Asp.Versioning;
using DigiBank.MS.Account.Service.Interfaces;
using DigiBank.NuGet.API;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DigiBank.MS.Account.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ApiController<AccountResponse>
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
            _accountService = accountService;
    }

    [HttpGet, MapToApiVersion("1.0")]
    [SwaggerOperation(
        Summary = "Get all Acounts with pagination.",
        Description = @"Get all Accounts with pagination.",
        Tags = new[] { "Accounts" }
        )]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
}
