using Microsoft.AspNetCore.Mvc;
using PlantShop.Services;
using Microsoft.AspNetCore.Authorization;

namespace PlantShop.Controllers;

//[Authorize]
public class TestController : Controller
{
    IUserService _userService;

    public TestController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Test()
    {
        var users = _userService.GetUsers();
        return View(users);
    }
}


