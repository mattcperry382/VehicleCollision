using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public class Account
    {//Model for user account info

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
