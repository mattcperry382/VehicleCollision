using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public interface ICollisionRepository
    {
        IQueryable<Crash> Crashes { get; }

        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void DeleteCrash(Crash c);
    }
}
