using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SitnEatV2.Models;
using static System.Formats.Asn1.AsnWriter;

namespace SitnEat.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		private readonly AuthDbContext _authDbContext;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		public AccountController(AuthDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_authDbContext = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		

		[HttpGet]
		public IActionResult Register()
		{
			return View(new Register());
		}



		[HttpPost]
		public async Task<IActionResult> Register(Register model)
		{
       
            if (ModelState.IsValid)
			{
				var user = new IdentityUser
				{
					UserName = model.Email,
					Email = model.Email
				};

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
        public async Task<IActionResult> LoginAsync(User LogModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(LogModel.Email);
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

    }
}
