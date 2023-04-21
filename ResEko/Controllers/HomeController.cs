using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ResEko.Models;
//using ResEko.Views.Home;
using System;
using System.Diagnostics;

namespace ResEko.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Restaurang()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}