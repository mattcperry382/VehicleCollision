using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public int CrashId { get; set; }
        public DateTime CrashDatetime {  get; set; }
        [Required]
        public string Route { get; set; }
        public float Milepoint { get; set; }
        public float LatUtmY { get; set; }
        public float LongUtmX { get; set; }
        [Required]
        public string MainRoadName { get; set; }
        public string City { get; set; }
        public string CountyName { get; set; }
        public int CrashSeverityId { get; set; }
        public CrashSeverity CrashSeverity { get; set; }
        [Required]
        public bool WorkZoneRelated { get; set; }
        [Required]
        public bool PedestrianInvolved { get; set; }
        [Required]
        public bool BicyclistInvolved { get; set; }
        [Required]
        public bool MotorcycleInvolved { get; set; }
        [Required]
        public bool ImproperRestraint { get; set; }
        [Required]
        public bool Unrestrained { get; set; }
        [Required]
        public bool Dui { get; set; }
        [Required]
        public bool IntersectionRelated { get; set; }
        [Required]
        public bool WildAnimalRelated { get; set; }
        [Required]
        public bool DomesticAnimalRelated { get; set; }
        [Required]
        public bool OverturnRollover { get; set; }
        [Required]
        public bool CommercialMotorVehInvolved { get; set; }
        [Required]
        public bool TeenageDriverInvolved { get; set; }
        [Required]
        public bool OlderDriverInvolving { get; set; }
        [Required]
        public bool NightDarkCondition { get; set; }
        [Required]
        public bool SingleVehicle { get; set; }
        [Required]
        public bool DistractedDriving { get; set; }
        [Required]
        public bool DrowsyDriving { get; set; }
        [Required]
        public bool RoadwayDeparture { get; set; }

    }
}
