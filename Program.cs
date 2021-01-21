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
            Console.WriteLine("|                         |");
            Console.WriteLine("| github.com/             |");
            Console.WriteLine("|      altered-existence/ |");
            Console.WriteLine("| ESO-Companion           |");
            Console.WriteLine("|                         |");
            Console.WriteLine("|=========================|");
            Console.WriteLine("|      G. Chisenhall      |");
            Console.WriteLine("| gchisenhall33@gmail.com |");
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