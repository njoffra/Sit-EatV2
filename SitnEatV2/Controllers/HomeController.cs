using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SitnEatV2.Models;
using System.Diagnostics;

namespace SitnEatV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AuthDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostComment(string message)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {

                var comment = new Comment
                {
                    Message = message,
                    Created = DateTime.Now,
                    Name = user.FirstName,
                    LastName = user.LastName
                };

                _context.Comments.Add(comment);
                _context.SaveChanges();

                return RedirectToAction("Comments", "Home");
            }

            return RedirectToAction("Error");
        }
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public IActionResult Comments()
        {

            var comments = _context.Comments.ToList();

            return View(comments);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Comments", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}