using DAL.Data;
using HRApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;


namespace HRApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HRManagementDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public HomeController(ILogger<HomeController> logger, HRManagementDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Applications", "Admin");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CompanyForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CompanyForm(CompanyViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            var company = new Company
            {
                CompanyId = Guid.NewGuid(),
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ApplicantId = user.Id
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Application Submitted!";
            return View();



            /*
            //if (ModelState.IsValid)
            //{
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View(model);
                }

                var company = new Company
                {
                    CompanyId = Guid.NewGuid(),
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ApplicantId = user.Id
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Application Submitted!";
                return View();
            //}

            //return View(model);
            */
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
