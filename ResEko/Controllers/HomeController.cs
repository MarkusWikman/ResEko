using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ResEko.Models;
using ResEko.Views.Home;
using System;
using System.Diagnostics;
using System.Net;

namespace ResEko.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly AccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext, AccountService accountService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _accountService = accountService;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Action metod för att skapa en admin. Bara lägg till en URL till den. Och sedan gå till den URLen så kommer en ny admin att skapas.
        public async Task<IActionResult> CreateAdmin()
        {
            // Check if the "Admin" role exists, and create it if necessary
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Check if the admin user exists, and create it if necessary
            var adminUser = await _userManager.FindByNameAsync("admin@reseko.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@reseko.com",
                    Email = "admin@reseko.com"
                };
                var result = await _userManager.CreateAsync(adminUser, "Admin!23");
                if (!result.Succeeded)
                {
                    return new StatusCodeResult(500); 
                }
            }

            // Add the admin user to the "Admin" role, if they are not already in the role
            if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            return Content("Admin user created successfully");
        }
    

    public IActionResult Index()
        {
            return View();
        }
        public IActionResult Restaurang()
        {
            return View();
        }
        public IActionResult Restaurangverksamhet()
        {
            return View();
        }
        public IActionResult Konceptinnovation()
        {
            return View();
        }
        public IActionResult Restaurangutbildning()
        {
            return View();
        }
        public IActionResult Redovisning()
        {
            return View();
        }
        [HttpGet("/Konsultation")]
        public IActionResult Konsultation()
        {
            return View();
        }

        [HttpPost("/Konsultation")]
        public IActionResult Konsultation(Customer customer)
        {

            if (ModelState.IsValid)
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Tack));
            }
            return View();
        }
        [HttpGet("/Tack")]
        public IActionResult Tack()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        [HttpGet("/Adminlogin")]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost("/AdminLogin")]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM)
        {

            var adminUser = await _userManager.FindByNameAsync(loginVM.Email);
            if (adminUser == null || !await _userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                return RedirectToAction(nameof(AdminLogin));
            }

            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Admin));
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(nameof(AdminLogin));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Admin()
        {
            var customers = _dbContext.Customers.ToList();
            return View(customers);
        }
        [HttpPost("/Customer/Delete/{customerId}")]
        public ActionResult Delete(int customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Admin));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}