using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Models;
using PlantShop.Services;

namespace PlantShop.Controllers;

public class HomeController : Controller
{
    IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
