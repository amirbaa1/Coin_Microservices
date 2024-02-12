using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}