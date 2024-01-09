using ExpensesWebApp.Models;
using ExpensesWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, model.RememberMe, false);

                if (result.Succeeded)
                {
                    TempData["success"] = "Você está logado.";
                    return RedirectToAction("Index", "Home");
                }
                TempData["error"] = "Tentativa de login inválida.";
                ModelState.AddModelError("LoginFailure", "Tentativa de login inválida.");

                return View(model);
            }


            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
