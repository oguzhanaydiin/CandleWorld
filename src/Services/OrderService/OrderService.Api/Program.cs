using Microsoft.AspNetCore;
using OrderService.Api.Extensions;
using OrderService.Infrastructure.Context;
namespace OrderService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = BuildWebHost(GetConfiguration(), args);
            host.MigrateDbContext<OrderDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<OrderDbContext>>();
                var dbContextSeeder = new OrderDbContextSeed();
                dbContextSeeder.SeedAsync(context, logger)
                    .Wait();
            });

            host.Run();
        }

        static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                })
                .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
            .UseStartup<Startup>()
            .Build();
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}