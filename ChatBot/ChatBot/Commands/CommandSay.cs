using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandSay : CommandBase, ICommand
    {
        public CommandSay(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            string message = e.Command.ChatMessage.Message;
            message = message.Substring(11, message.Length - 11);
            Console.WriteLine("Vector should say the following: " + message);
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
