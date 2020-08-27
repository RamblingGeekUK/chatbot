using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {

            //configure console logging
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Build())
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

            Log.Information("Chatbot Started");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                { 
                    services.AddSingleton<ITwitchBotService, TwitchBot>();
                    services.AddSingleton<IDiscordBotService, DiscordBot>();
                })
                .UseSerilog()
                .Build();

            //var serviceProvider = new ServiceCollection()
            //    .AddSingleton<ITwitchBotService, TwitchBot>()
            //    .AddSingleton<IDiscordBotService, DiscordBot>()
            //    .BuildServiceProvider();


            // var connection = new HubConnectionBuilder()
            // .WithUrl("https://localhost:5001/chathub")
            // .Build();
            // connection.StartAsync().Wait();

            // connection.InvokeCoreAsync("SendMessage", args: new[] { e.ChatMessage.Message, e.ChatMessage.Username });

            var TwitchBot = ActivatorUtilities.CreateInstance<TwitchBot>(host.Services);
            TwitchBot.Start();


            var DiscordBot = ActivatorUtilities.CreateInstance<DiscordBot>(host.Services);
            _ = DiscordBot.Start();


            // Keep the program running until a esc key is presssed. 
            ConsoleKeyInfo info = Console.ReadKey();
          
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}