using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandUnLurk : CommandBase, ICommand
    {
        public CommandUnLurk(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
            var message = $"Welcome back { e.Command.ChatMessage.Username }, happy to have you back in chat!";
            this.SendMessage(e.Command.ChatMessage.Channel, message);
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
