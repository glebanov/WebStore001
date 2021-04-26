using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        //Нам нужен регистр в представлении контроллера Account
        #region Register
        [AllowAnonymous] //Дает возможность предоставить доступ для анонимных пользователей в заблокированном контроллере
        public IActionResult Register() => View(new RegisterUserViewModel());
        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken/*, ActionName("Register")*/]
        public async Task<IActionResult> Register/*Async*/(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Регистрация пользователя {0}", Model.UserName);

            var user = new User
            {
                UserName = Model.UserName
            };

            using (_Logger.BeginScope("Регистрация пользователя {0}", Model.UserName))
            {
                var registration_result = await _UserManager.CreateAsync(user, Model.Password);
                if (registration_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                    await _UserManager.AddToRoleAsync(user, Role.Users);
                    _Logger.LogInformation("Пользователь {0} наделён ролью {1}", user.UserName, Role.Users);

                    await _SignInManager.SignInAsync(user, false);

                    _Logger.LogInformation("Пользователь {0} вошёл в систему сразу после регистрации", user.UserName);

                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("Ошибка при регистрации пользователя {0}: {1}",
                user.UserName,
                string.Join(",", registration_result.Errors.Select(e => e.Description)));
               
                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(Model);
        }
        #endregion

        #region Login

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else 
                true
#endif
                );

            if (login_result.Succeeded)
            {
                return LocalRedirect(Model.ReturnUrl ?? "/");
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль!");

            return View(Model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}