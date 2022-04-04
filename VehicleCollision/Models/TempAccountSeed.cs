using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models
{
    public static class TempAccountSeed
    {

        private const string adminUser = "Admin1234";
        private const string adminPassword = "Admin123&money!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            IdentityContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<IdentityContext>();
            if(context.Database.GetPendingMigrations().Any())
                {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if(user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "admin@yeet.com";
                user.PhoneNumber = "1111111";

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
