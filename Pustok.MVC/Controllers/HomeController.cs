using Microsoft.AspNetCore.Mvc;

namespace Pustok.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
