using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models;
using VehicleCollision.Services;

// INTEX 2022 UTAH Motor Vehicle Collisions Site
// Alicia Lane, Jacob Donaldson, Jessica Kinghorn, Matt Perry
//4/8/2022
//Version 0.9

//This site allows users to securly view, predicte, and work with utah vehicla collision data
namespace VehicleCollision
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        private string _IdentityDBConnection = null;
        private string _CollisionDBConnection = null;
        private string _MailKit_SMTP_Account = null;
        private string _MailKit_SMTP_Address = null;
        private string _MailKit_SMTP_Password = null;
        private string _MailKit_SMTP_Port = null;
        private string _MailKit_SMTP_SenderEmail = null;
        private string _MailKit_SMTP_SenderName = null;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Secrets Manager
            if (CurrentEnvironment.IsDevelopment())
            {
                _IdentityDBConnection = Configuration["ConnectionStrings:IdentityDBConnection"];
                _CollisionDBConnection = Configuration["ConnectionStrings:CollisionDBConnection"];
                _MailKit_SMTP_Account = Configuration["SMTP:Account"];
                _MailKit_SMTP_Address = Configuration["SMTP:Address"];
                _MailKit_SMTP_Password = Configuration["SMTP:Password"];
                _MailKit_SMTP_Port = Configuration["SMTP:Port"];
                _MailKit_SMTP_SenderEmail = Configuration["SMTP:SenderEmail"];
                _MailKit_SMTP_SenderName = Configuration["SMTP:SenderName"];
            }
            else
            {
                _IdentityDBConnection = Environment.GetEnvironmentVariable("IdentityDBConnection");
                _CollisionDBConnection = Environment.GetEnvironmentVariable("CollisionDBConnection");
                _MailKit_SMTP_Account = Environment.GetEnvironmentVariable("MailKit_SMTP_Account");
                _MailKit_SMTP_Address = Environment.GetEnvironmentVariable("MailKit_SMTP_Address");
                _MailKit_SMTP_Password = Environment.GetEnvironmentVariable("MailKit_SMTP_Password");
                _MailKit_SMTP_Port = Environment.GetEnvironmentVariable("MailKit_SMTP_Port");
                _MailKit_SMTP_SenderEmail = Environment.GetEnvironmentVariable("MailKit_SMTP_SenderEmail");
                _MailKit_SMTP_SenderName = Environment.GetEnvironmentVariable("MailKit_SMTP_SenderName");
            }

            //GDPR COOKIE STUFF
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.Secure = CookieSecurePolicy.Always;
            
                
            });
        
            services.AddDbContext<IdentityContext>(options =>
                options.UseMySql(_IdentityDBConnection));
            services.AddDbContext<CollisionContext>(options =>
                options.UseMySql(_CollisionDBConnection));
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
            }).AddInMemoryKeyStore();

      


            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //SMTP EMAIL SERVICE
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.Configure<MailKitOptions>(options =>
            {
                options.Host_Address = _MailKit_SMTP_Address;
                options.Host_Port = Convert.ToInt32(_MailKit_SMTP_Port);
                options.Host_Username = _MailKit_SMTP_Account;
                options.Host_Password = _MailKit_SMTP_Password;
                options.Sender_EMail = _MailKit_SMTP_SenderEmail;
                options.Sender_Name = _MailKit_SMTP_SenderName;
            });
            //services.AddTransient<IEmailSender, YourSmsSender>();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(30);
            });
            services.AddSingleton<InferenceSession>(
              new InferenceSession("Models/DataAnalytics/XGBoostClassifierModel.onnx")
            );
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
            app.UseCookiePolicy();

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
                ctx.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; font-src 'self' data: fonts.gstatic.com;style-src 'self' 'unsafe-inline' fonts.googleapis.com; img-src 'self' data:; frame-src https://airtable.com/ http://utmvc.com/CrashesByHour.png http://utmvc.com/MonthsOf2019.png http://utmvc.com//TeenageDriver.png") ;
                await next();
            });

            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; preload");
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
        }
    }
}
