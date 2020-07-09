
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
                .AddSingleton<ITwitchBotService, Bot>()
                .AddSingleton<IDiscordBotService, DiscordBot>()
                .BuildServiceProvider();

            var bot = serviceProvider.GetService<ITwitchBotService>();
            bot.BotStart();

            var Discordb = serviceProvider.GetService<IDiscordBotService>();
            Discordb.DBot();


            // Keep the program running until a esc key is presssed. 
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Stopping Application - Escape Key Pressed");
                
            }

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");
       
        }
    }
}