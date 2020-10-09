﻿using System;
using System.Threading.Tasks;
using ChatBot.Base;
using DSharpPlus;
using Serilog;

namespace ChatBot
{

    public class DiscordBot : IDiscordBotService 
    {
        private static DiscordClient _discordClient;
        public async Task Start()
        {
            Log.Information("Discord Bot Started");
            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = Environment.GetEnvironmentVariable("DiscordToken"),
                TokenType = TokenType.Bot

            });

            _discordClient.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!ping"))
                    await e.Message.RespondAsync("pong!");
            };

            _discordClient.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!vector-say"))
                {
                    await e.Message.RespondAsync("why are you trying to make me talk in here? :-)");

                }

            };

            await _discordClient.ConnectAsync();
            // Discord StreamLinks ID
            //await PostMessage(729021058568421386, "Hello World");
            //await Task.Delay(-1);
        }

        public static async Task PostMessage(string DisplayName, ulong channelId, string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(channelId).Result;
            await _discordClient.SendMessageAsync(discordChannel, $"{DisplayName} posted a link in chat {text}. {DateTime.UtcNow.ToString()}");
        }
    }
}
