using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public class EFCollisionRepository : ICollisionRepository
    {
        private CollisionContext context { get; set; }
        public EFCollisionRepository(CollisionContext temp)
        {
            context = temp;
        }
        public IQueryable<Crash> Crashes => context.Crashes;

        public IQueryable<CrashSeverity> CrashSeverity => context.CrashSeverity;

        public void SaveCrash(Crash c)
        {
            context.SaveChanges();
        }

        public void CreateCrash(Crash c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            context.Remove(c);
            context.SaveChanges();
        }
    }
}
