using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SitnEatV2.Models;
using System.Text.Encodings.Web;

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
        private readonly IEmailSender _emailSender;
        public AccountController(AuthDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _authDbContext = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
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

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Korisnik ne postoji!");
                    
                }
                else
                {
                    var result = await signInManager.PasswordSignInAsync(user, LogModel.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                //ModelState.AddModelError("", "Netačan email ili šifra!");
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword (ForgotPassword forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Korisnik ne postoji!");
                }
                else
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Reset password token", $"Da promijenite lozinku, <a href='{HtmlEncoder.Default.Encode(callback)}'>kliknite ovdje</a>.");
                    await _emailSender.SendEmailAsync(message);
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                
            }
            return View(forgotPasswordModel);
            
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(resetPasswordModel.Email);
                if(user == null)
                {
                    ModelState.AddModelError("Email", "Nije moguće poslati zahtjev.");
                }
                else
                {
                    var resetPasswordResult = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
                    if (resetPasswordResult.Succeeded)
                    {
                        return RedirectToAction(nameof(ResetPasswordConfirmation));
                    }
                    else
                    {
                        foreach (var error in resetPasswordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        return View();
                    }
                }
                
            }
            
            return View(resetPasswordModel);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        



    }
}
