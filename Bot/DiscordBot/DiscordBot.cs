using ChatBot.Helpers;
using DSharpPlus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace ChatBot
{

    public class DiscordBot : IDiscordBotService
    {
        private static DiscordClient _discordClient;
        private TwitchClient client;
        private Dictionary<string, ICommand> commands;

        public async Task Start()
        {

            this.commands = CommandHelper.GetCommands(client);

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
                    //CommandAnnounce(client).Execute(vtalktext, e);
                    TwitchLib.Client.Events.OnChatCommandReceivedArgs te = new TwitchLib.Client.Events.OnChatCommandReceivedArgs();
                    
                    commands[e.Message.Content.ToLower()].Execute(te);

                }
            };
            await _discordClient.ConnectAsync();
        }

        public static async Task PostMessage(string DisplayName, ulong channelId, string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(channelId).Result;
            await _discordClient.SendMessageAsync(discordChannel, $"{DisplayName} posted a link in chat {text}. {DateTime.UtcNow.ToString()}");
        }
    }
}
