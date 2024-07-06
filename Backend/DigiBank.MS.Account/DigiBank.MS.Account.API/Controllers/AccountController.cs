using Microsoft.AspNetCore.Mvc;

namespace DigiBank.MS.Account.API.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
