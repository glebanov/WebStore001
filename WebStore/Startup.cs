using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Infrastructure.Services.InCookies;
using System;

namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(Configuration.GetConnectionString("Sqlite"))); //Строка подключения Sqlite и подключить в NuGet
            services.AddDbContext<WebStoreDB>(opt =>
             opt.UseSqlServer(Configuration.GetConnectionString("Default"))
                .UseLazyLoadingProxies()
             );
            services.AddTransient<WebStoreDbInitializer>();

            services.AddIdentity<User, Role>(/*opt => { }*/)
             .AddEntityFrameworkStores<WebStoreDB>()
             .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });


            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();  //Указываем интерфейс и реализацию
            //services.AddTransient<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, SqlProductData>();
            services.AddTransient<ICartService, InCookiesCartService>();
      
            services
                   .AddControllersWithViews()
                   .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {

            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/welcome");

            app.UseMiddleware<TestMiddleware>();

            app.MapWhen(
                context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
                context => context.Run(async request => await request.Response.WriteAsync("Hello with id ==5!"))
                );
            app.Map("/hello", context => context.Run(async request => await request.Response.WriteAsync("Hello!!!")));


            app.UseEndpoints(endpoints =>
          {
              //Маршрут для Areas/Admin
              endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

              //Маршрут по умолчанию
              endpoints.MapControllerRoute(
              "default",
              "{controller=Home}/{action=Index}/{id?}");
          });
        }
    }
}
