using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading;

namespace ChatBot
{
    partial class Program
    {
        static void Main(string[] args)
        {
            new Bot();
            CreateWebHostBuilder(args).Build().Run();


           
            Thread.Sleep(Timeout.Infinite);
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();
    }
}