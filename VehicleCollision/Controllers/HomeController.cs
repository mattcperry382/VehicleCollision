using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;

namespace VehicleCollision.Controllers
{
    public class HomeController : Controller
    {
        private ICollisionRepository _repo { get; set; }

        public HomeController(ICollisionRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            var crashes = _repo.Crashes
                .Include(x => x.CrashSeverity)
                //.Where(x => x.CrashSeverity.Description == description || description == null)
                .ToList();

            return View(crashes);
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
