using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;

namespace VehicleCollision.Components
{
	public class CrashSeverityViewComponent : ViewComponent
	{
		private ICollisionRepository _repo { get; set; }
		public CrashSeverityViewComponent(ICollisionRepository temp)
		{
			_repo = temp;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectCrashSeverity = RouteData?.Values["severity"];

			var crashseverity = _repo.CrashSeverity
			.Select(x => x.Description)
			.Distinct()
			.OrderBy(x => x);
			return View(crashseverity);
		}
	}
}


