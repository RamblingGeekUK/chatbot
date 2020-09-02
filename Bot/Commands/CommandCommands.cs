
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandCommands : CommandBase, ICommand
    {
        public CommandCommands(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            var msg = new List<string>
            {
                "Alive",
                "Announce",
                "Attention",
                "Commands",
                "Lurk",
                "Vector-Say your text",
                "Vector-Joke",
                "Unlunk"
            };

            foreach (var message in msg)
            {
                this.MessageChat(e.Command.ChatMessage.Channel, message);
                new CommandAnnounce(client).Execute(message, e);
            }
        }
    }
}
