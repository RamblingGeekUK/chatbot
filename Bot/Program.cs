
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace ChatBot
{
    partial class Program
    {
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ITwitchBotService, TwitchBot>()
                .AddSingleton<IDiscordBotService, DiscordBot>()
                .BuildServiceProvider();

            var TwitchBot = serviceProvider.GetService<ITwitchBotService>();
            TwitchBot.Start();

            var DiscordBot = serviceProvider.GetService<IDiscordBotService>();
            DiscordBot.Start();

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            // Keep the program running until a esc key is presssed. 
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Stopping Application - Escape Key Pressed");

            }

        }
    }
}