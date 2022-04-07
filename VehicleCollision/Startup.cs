using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;
using VehicleCollision.Services;


namespace VehicleCollision
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:IdentityDBConnection"]));
            services.AddDbContext<CollisionContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:CollisionDBConnection"]));
            services.AddScoped<ICollisionRepository, EFCollisionRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;


            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
            //var cn = Configuration.GetConnectionString("DefaultConnection");

            services.AddAuthentication();

            services.AddFido(options =>
            {
                options.Licensee = "DEMO";
                options.LicenseKey =
                    "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjItMDUtMDRUMDE6MDA6MDMuNTMzNDU5OSswMDowMCIsImlhdCI6IjIwMjItMDQtMDRUMDE6MDA6MDMiLCJvcmciOiJERU1PIiwiYXVkIjo2fQ==.fKuw59Ym4xwWB6dSYpfENPLblFROIzjj6P5LehisgGVioN+9H1K6wKdiP5aIHuJgLgVbx02emmSK9E4navvKR4/SxXabY1ebMD8uTzqfzfsPZA8zPONiH6qYwdciSIPpdNOQEbYjdJgRmECyfj3P5pxZjSYaqQoJacKf1ex30ULOXVLopi656kNKB3EIK5Pbvs+nNM97hfbBXLTFsvlAjsMABbQ4gZ4PCFTjTlsQxolze7CYfZTv0JUBmdIsfvQ1KpHvXFCogQbQVIT8sSPUaZjLfJZycnkMK/K9PWvedcmHUDVb7RK39W6O0XWRLjwDJLRwTAUVt0lsQJutO1gSMlbYoLC3L4fU5sUscu0cFhHm39Fe9AnN3ltDu/x0yyjRNzdghSdFC+1xz5Oo1ZBXkEc6PCX47KQ44jWvffUWsf2jLeR9LeUeKEQWoEX/J9gtKPtjxyl0WHo0NDf0CkauaMEQ1nPqH65CmwqeSKSvk1r0w7im2a5JWknM3kt6EPRJ5YIca8k4U2ONiyND0paUBcN+40cQEOJlmplLYS7r8jt6LRTNUrrOtY0EZ1w5A1ZXF5vNFx48wn+r1vURGcqGbqzdTHmePd2hkOFK7h8+vloqbNZ5XvoD+I1bkm+oFqZcRdcE0xW+f1hKAE/QpvFsZPY+M37bXjPo1Hob1llKT2o=";
            }).AddInMemoryKeyStore();
            //.AddEntityFrameworkStore(options =>
            //options.UseMySql(Configuration["ConnectionStrings:IdentityDBConnection"]));

      


            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //SMTP EMAIL SERVICE
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.Configure<MailKitOptions>(options =>
            {
                options.Host_Address = Configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Host_Port = Convert.ToInt32(Configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Host_Username = Configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Host_Password = Configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.Sender_EMail = Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.Sender_Name = Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
            });
            //services.AddTransient<IEmailSender, YourSmsSender>();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(30);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
          
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //authentication and Identity
            app.UseAuthentication();
            app.UseAuthorization();

            //X-Xss Protection
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });

            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; font-src 'self' data: fonts.gstatic.com;style-src 'self' 'unsafe-inline' fonts.googleapis.com; img-src 'self' data:") ;
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            });
            TempAccountSeed.EnsurePopulated(app);
        }
    }
}
