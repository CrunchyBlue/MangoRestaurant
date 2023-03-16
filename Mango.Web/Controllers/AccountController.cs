using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers;

/// <summary>
/// The account controller.
/// </summary>
public class AccountController : Controller
{
    /// <summary>
    /// The access denied.
    /// </summary>
    /// <returns></returns>
    public IActionResult AccessDenied()
    {
        return View();
    }
}