using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           //.ConfigureLogging((host, log) => log
           //   .ClearProviders()
           //   .AddEventLog()
           //   .AddConsole()
           //   .AddFilter/*<ConsoleLoggerProvider>*/("Microsoft.Hosting", LogLevel.Error)
           //   .AddFilter((category, level) => !(category.StartsWith("Microsoft") && level >= LogLevel.Warning))
           //)
           .ConfigureWebHostDefaults(host => host
              .UseStartup<Startup>()
           );
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
