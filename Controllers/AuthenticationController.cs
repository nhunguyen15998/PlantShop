using Microsoft.AspNetCore.Mvc;
using PlantShop.FormValidations;
using PlantShop.Models;
using PlantShop.Services;

namespace PlantShop.Controllers
{
    public class AuthenticationController : Controller
    {
        IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;

        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpAction([Bind("FirstName, LastName, Email, Phone, Password, RepeatPassword")] RegisterForm user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    UserModel created = new UserModel();
                    created.FirstName = user.FirstName;
                    created.LastName = user.LastName;
                    created.Email = user.Email;
                    created.Phone = user.Phone;
                    created.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _userService.createUser(created);
                    return RedirectToAction("SignIn", "Authentication");
                }
                return View("SignUp", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }


    }
}