using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Gateway
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
                    webBuilder.ConfigureAppConfiguration((builder) =>
                    {
                        builder.SetBasePath(Directory.GetCurrentDirectory())
                            //.AddJsonFile("config/appsettings.Kubernetes.json", true) TODO enable when using Kubernetes
                            //.AddJsonFile("config/ocelot.Kubernetes.json", true) TODO enable when using Kubernetes
                            .AddJsonFile("ocelot.Development.json")
                            .AddJsonFile("appsettings.Development.json")
                            .AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
