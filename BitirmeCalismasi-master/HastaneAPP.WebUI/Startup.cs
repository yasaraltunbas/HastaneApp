using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HastaneApp.WebUI.Middlewares;
using HastaneAPP.WebUI.Services.EmailServices;
using HastaneAPP.WebUI.Services.SmsServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HastaneAPP.WebUI.Models.Identity;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using HastaneAPP.WebUI.Services.AppDbService.Concrete.EfCore;

namespace HastaneAPP.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IHastaRepository,EfCoreHastaRepository>();
            services.AddScoped<IOperasyonRepository,EfCoreOperasyonRepository>();
            services.AddScoped<IExtraHastalikRepository,EfCoreExtraHastalikRepository>();


            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("ProjectDBConnection")));
          
          
    

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // password

                // sayısal değer ister
                options.Password.RequireDigit = false;
                // küçük karakter
                options.Password.RequireLowercase = false;
                // en az 6 karakter
                options.Password.RequiredLength = 6;
                //alfa numeric olmayacak
                options.Password.RequireNonAlphanumeric = false;
                // büyük karaktr
                options.Password.RequireUppercase = false;

                // 5 kere yanlış şifre girerse 30 saniye beklet
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //yeni kullanıcı içinde geçerli 
                options.Lockout.AllowedForNewUsers = true;

                // username' de olmayacak harfleri seçebiliriz
                options.User.AllowedUserNameCharacters = "";
                // mail adresi kullanılıyorsa yeni üye açmaz 
                options.User.RequireUniqueEmail = true;

                // giriş için email onayı               
                options.SignIn.RequireConfirmedEmail = false;
                // giriş için telefon onayı
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
            new SmtpEmailSender(
                Configuration["EmailSender:Host"],
                Configuration.GetValue<int>("EmailSender:Port"),
                Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                Configuration["EmailSender:UserName"],
                Configuration["EmailSender:Password"])
           );
           services.AddScoped<ISmsSender, VonageSmsSender>(i =>
            new VonageSmsSender(
                Configuration["SmsSender:ApiKey"],
                Configuration["SmsSender:ApiSecret"],
                Configuration["SmsSender:From"])
           );

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // wwwroot
            //app.CustomStaticFiles();
            app.UseAuthentication();
            // app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                // routes.MapRoute(
                //     name: "crypto",
                //     template: "{param?}",
                //     defaults: new { controller = "Home", Action = "Index" }
                // );


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Hasta}/{action=List}/{id?}"
                );
            });


        }
    }
}
