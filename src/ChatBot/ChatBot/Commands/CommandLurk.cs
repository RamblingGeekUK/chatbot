using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandLurk : CommandBase, ICommand
    {
        public CommandLurk(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            var message = $" { e.Command.ChatMessage.Username } is Lurking for a little while..." ;
            this.MessageChat(e.Command.ChatMessage.Channel, message);
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
