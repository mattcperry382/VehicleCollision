using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleCollision.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the VehicleCollisionUser class
    public class VehicleCollisionUser : IdentityUser
    {

        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
