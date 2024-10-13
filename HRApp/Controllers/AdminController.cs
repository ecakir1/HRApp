using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace HRApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public AdminController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToExecutive(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Company Executive");
            }

            return RedirectToAction("ManageUsers");
        }
    }
}
