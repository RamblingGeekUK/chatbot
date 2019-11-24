using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandFreePlay : CommandBase, ICommand
    {
        public CommandFreePlay(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            var message = $" setting FreePlay to true" ;
            //VectorFreePlayPost(false);

            this.MessageChat(e.Command.ChatMessage.Channel, message);
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
