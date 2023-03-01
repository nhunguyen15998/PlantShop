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
            if(HttpContext.Session.GetString("CurrentPhone") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpAction(RegisterForm user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check unique phone
                    UserModel? existingUser = _userService.CheckExistingUserPhone(user.Phone);
                    if(existingUser != null) 
                    {
                        TempData["Error"] = "Phone is already existed";
                        return View("SignUp", user);
                    }
                    bool isRegistered = _userService.Register(user);
                    if(isRegistered)
                    {
                        TempData["Success"] = "Successfully registered";
                        return RedirectToAction("SignIn", "Authentication");
                    }
                    TempData["Error"] = "Invalid login, please try again";
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
            if(HttpContext.Session.GetString("CurrentPhone") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignInAction(SignInForm user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var validUser = _userService.VerifyUser(user);
                    if (validUser == null)
                    {
                        TempData["Error"] = "Invalid user";
                        return View("SignIn");
                    }
                    HttpContext.Session.SetString("CurrentPhone", validUser.Phone!);
                    HttpContext.Session.SetString("CurrentUser", validUser.FirstName + " " + validUser.LastName);
                    HttpContext.Session.SetString("CurrentEmail", validUser.Email!);
                    // HttpContext.Session.SetString("CurrentAvatar", validUser.Avatar!);
                    HttpContext.Session.SetString("CurrentId", validUser.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                return View("SignIn", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult SignOut()
        {
            if(HttpContext.Session.GetString("CurrentPhone") != null)
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}