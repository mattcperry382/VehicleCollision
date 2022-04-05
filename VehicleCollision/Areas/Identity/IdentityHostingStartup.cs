using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: HostingStartup(typeof(VehicleCollision.Areas.Identity.IdentityHostingStartup))]
namespace VehicleCollision.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<VehicleCollisionIdentityContext>(options =>
                //    options.UseSqlite(
                //        context.Configuration.GetConnectionString("VehicleCollisionIdentityContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<VehicleCollisionIdentityContext>();
            });
        }
    }
}