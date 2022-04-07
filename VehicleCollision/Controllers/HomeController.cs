using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;
using VehicleCollision.Models.ViewModels;

namespace VehicleCollision.Controllers
{
    public class HomeController : Controller
    {
        private ICollisionRepository _repo { get; set; }

        public HomeController(ICollisionRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index(string severity, int pageNum = 1)
        {
            //int pageSize = 25;
            //var crashes = new CrashesViewModel
            //{
            //    Crashes = _repo.Crashes
            //    //.Where(cs => cs.CrashSeverity == severity || severity == null)
            //    .OrderBy(c => c.CrashId)
            //    .Skip((pageNum - 1) * pageSize)
            //    .Take(10)
            //    , //pageSize

            //    PageInfo = new PageInfo
            //    {
            //        TotalNumCrashes = 100,
            //        CrashesPerPage = pageSize,
            //        CurrentPage = pageNum
            //    }
            //};

            return View();

        }
        public IActionResult CollisionData(string severity, int pageNum = 1)
        {

            int pageSize = 25;
            var crashes = new CrashesViewModel
            {
                Crashes = _repo.Crashes
                .Include(x => x.CrashSeverity)
                .Where(x => x.CrashSeverity.Description == severity || severity == null)
                .OrderBy(c => c.CrashId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                , //pageSize

                PageInfo = new PageInfo
                {

                    TotalNumCrashes = (severity == null ? _repo.Crashes.Count() / 10000 : _repo.Crashes.Where(x => x.CrashSeverity.Description == severity).Count() / 10000),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            //ViewBag.CrashSeverity = _repo.CrashSeverities.ToList();

            return View(crashes);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [Authorize(Roles = "Community, Administrator")]
        public IActionResult Community()
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
