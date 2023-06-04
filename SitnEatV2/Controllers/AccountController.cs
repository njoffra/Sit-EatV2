using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SitnEatV2.Models;

namespace SitnEat.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(AuthDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authDbContext = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        protected ILookupNormalizer normalizer;

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };
                //user.Email = userManager.NormalizeEmail(user.Email);


                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login LogModel)
        {
            //Console.WriteLine($"Email: {LogModel.Email}");
            //Console.WriteLine($"Password: {LogModel.Password}");
            if (ModelState.IsValid)
            {
                //var normalizedEmail = userManager.NormalizeEmail(LogModel.Username);
                var user = await userManager.FindByNameAsync(LogModel.Email);
                //bool isLockedOut = await userManager.IsLockedOutAsync(user);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, LogModel.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Netačan email ili šifra!");
            }

            return View(LogModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
