using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace ESOCompanion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("|=========================|");
            Console.WriteLine("|      ESO Companion      |");
            Console.WriteLine("|        Topher Lee       |");
            Console.WriteLine("| topher.lee.13@gmail.com |");
            Console.WriteLine("|=========================|");

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