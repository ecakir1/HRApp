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

        public IActionResult Index()
        {
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
            if (ModelState.IsValid)
            {
                // Admin kullanýcýsýný veritabanýndan çekin
                var adminUser = await _userManager.GetUsersInRoleAsync("Admin");
                var admin = adminUser.FirstOrDefault();
                if (admin == null)
                {
                    ModelState.AddModelError("", "Admin user not found.");
                    return View(model);
                }

                var company = new Company
                {
                    CompanyId = Guid.NewGuid(),
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    AdminID = admin.Id
                };

                _context.Companies.Add(company);
                _context.SaveChanges();

                ViewBag.Message = "Application Submitted!";
                return View();
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
