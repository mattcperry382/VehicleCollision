using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// INTEX 2022 UTAH Motor Vehicle Collisions Site
// Alicia Lane, Jacob Donaldson, Jessica Kinghorn, Matt Perry
//4/8/2022
//Version 0.9

//This site allows users to securly view, predicte, and work with utah vehicla collision data
namespace VehicleCollision
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
