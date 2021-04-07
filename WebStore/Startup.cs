using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using WebStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;


namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(Configuration.GetConnectionString("Sqlite"))); //Строка подключения Sqlite и подключить в NuGet
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default"))); //Указываем какой сервер используем
            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();  //Указываем интерфейс и реализацию
            services.AddTransient<IProductData, InMemoryProductData>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
          
            services
                   .AddControllersWithViews()
                   .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
           
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseWelcomePage("/welcome");

            app.UseMiddleware<TestMiddleware>();

            app.MapWhen(
                context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
                context => context.Run(async request => await request.Response.WriteAsync("Hello with id ==5!"))
                );
            app.Map("/hello", context => context.Run(async request => await request.Response.WriteAsync("Hello!!!")));


            app.UseEndpoints(endpoints =>
          {
            
              //Маршрут по умолчанию
              endpoints.MapControllerRoute(
              "default",
              "{controller=Home}/{action=Index}/{id?}");
          });
        }
    }
}
