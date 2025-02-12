using HubCommerce.AuthServices;
using HubCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;
using System.Security.Cryptography;

namespace HubCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly LoginService loginService;
        private readonly RegistrationService registrationService;

        public HomeController(ILogger<HomeController> logger,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager,LoginService loginService,RegistrationService registrationService) 
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.loginService = loginService;
            this.registrationService = registrationService;
        }
        [Authorize(policy: "RestrictBannedUser")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(policy: "RestrictBannedUser")]
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        // Used Method Parametr binding
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
               var result=  await loginService.LoginUser(model);
                if(!result.Succeeded)
                {
                    _logger.LogWarning($"User with email {model.Email }tried to log in but failed ");
                    ModelState.AddModelError("LoginFailed", "Email or password is incorrect");
                    return View(model);
                }
            }
            return View(model);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
               var result =  await registrationService.RegisterUser(model);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("RegisterFailed", "Something went wrong in register process , please try again later");
                    _logger.LogWarning($"User with email : {model.Email} failed to register");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
