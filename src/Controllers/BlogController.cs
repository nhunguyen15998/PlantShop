using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Models;
using PlantShop.Services;

namespace PlantShop.Controllers;

public class BlogController : Controller
{
    IUserService _userService;

    public BlogController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("blog/list")]
    public IActionResult BlogList()
    {
        return View();
    }

    [Route("blog/detail/{id}")]
    public IActionResult BlogDetail(int id)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
