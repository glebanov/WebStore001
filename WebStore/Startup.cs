using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebStore.Data;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using WebStore.Infrastructure.Services;
using WebStore.Models;


namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>(); ; //Указываем интерфейс и реализацию
            //services.AddMvc();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddTransient<>(); // Так регистрируем сервис, который не должен хранить состояние
            //services.AddScoped<>();    // Так регистрируем сервис, который должен помнить состояние на время обработки входящего подключения
            //services.AddSingleton<>(); // Так регистрируем сервис, хранящий состояние на всё время жизни приложения
            services
                   .AddControllersWithViews(/*opt => opt.Conventions.Add(new TestControllerModelConvention())*/)
                   .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IServiceProvider services*/)
        {
            //var employees1 = services.GetService<IEmployeesData>();
            //var employees2 = services.GetService<IEmployeesData>();

            //var hash1 = employees1.GetHashCode();
            //var hash2 = employees2.GetHashCode();

            //using (var scope = services.CreateScope())
            //{
            //    var employees3 = scope.ServiceProvider.GetService<IEmployeesData>();
            //    var hash3 = employees3.GetHashCode();
            //}
           
            
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
              endpoints.MapGet("/greetings", async context =>
              {
                  //await context.Response.WriteAsync(greetings);
                  await context.Response.WriteAsync(Configuration["Greetings"]);
              });
              //Маршрут по умолчанию
              endpoints.MapControllerRoute(
              "default",
              "{controller=Home}/{action=Index}/{id?}");
          });
        }
    }
}
