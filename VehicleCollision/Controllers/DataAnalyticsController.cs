using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Controllers
{
    public class DataAnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View("LandingPage");
        }
    }
}
