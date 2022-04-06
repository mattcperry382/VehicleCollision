using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> Crashes { get; set; }

        public IQueryable<CrashSeverity> CrashSeverity { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}