using ChatBot.Base;
using ChatBot.Helpers;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace ChatBot
{

    public class DiscordBot : IDiscordBotService
    {
        private static DiscordClient _discordClient;
        private readonly TwitchClient _twitch_client;

        private Dictionary<string, ICommand> TwitchCommands;
        static CommandsNextModule commands;

        public async Task Start()
        {

            this.TwitchCommands = CommandHelper.GetCommands(_twitch_client);


            _discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = Environment.GetEnvironmentVariable("DiscordToken"),
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Error
            });

            _discordClient.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("!ping"))
                    await e.Message.RespondAsync("pong!");
            };

            commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<ChatBot.DiscordBot.DiscordCommands>();
            await _discordClient.ConnectAsync();
        }

        public static async Task PostMessage(string DisplayName, ulong channelId, string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(channelId).Result;
            await _discordClient.SendMessageAsync(discordChannel, $"{DisplayName} posted a link in chat {text}. {DateTime.UtcNow.ToString()}");
        }

        private class DiscordCommands
        {
            
            [Command("vector-say")]
            public async Task VectorSay(CommandContext ctx)
            {
                await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");

                TwitchClient _twitch_client = null;
                new CommandAnnounce(_twitch_client).Execute($"{ctx.Message.Content.Remove(1, 10)}. That was a message from {ctx.User.Username}.");
                Log.Logger.Information("Message received from discord bot - I can't say any more");
                await ctx.Message.RespondAsync($"Your message has been sent");

                await ctx.Channel.SendFileAsync($"{Directory.GetCurrentDirectory()}\\image.jpg");
            }
        }
    }
}
