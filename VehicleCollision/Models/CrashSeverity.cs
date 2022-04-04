using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public class CrashSeverity
    {
        [Key]
        [Required]
        public int CrashSeverityId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
