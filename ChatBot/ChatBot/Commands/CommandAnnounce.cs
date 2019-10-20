using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandAnnounce : CommandBase
    {
        public CommandAnnounce(TwitchClient client)
            : base(client)
        {
        }

        public void Execute (string message, OnRaidNotificationArgs e)
        {
            this.MessageChat(e.Channel, message);
            this.Vector(message);
        }
        public void Execute(string message, OnMessageReceivedArgs e)
        {
            this.MessageChat(e.ChatMessage.Channel, message);
            this.Vector(message);
        }

        public void Execute(string message, OnChatCommandReceivedArgs e)
        {
            this.MessageChat(e.Command.ChatMessage.BotUsername, message);
            this.Vector(message);
        }

        ////public bool VectorAPICheck(OnJoinedChannelArgs e)
        ////{
        ////    string message = null;
        ////    this.MessageChat(e.Channel, message);
            
        ////    if (this.Vector(message))
        ////    {
        ////        Console.ForegroundColor = ConsoleColor.Green;
        ////        Console.WriteLine($"Call to Vector API with message : {message}");
        ////        Console.ForegroundColor = ConsoleColor.Gray;
        ////    }
        ////    else
        ////    {
        ////        Console.ForegroundColor = ConsoleColor.Red;
        ////        Console.WriteLine("Call to Vector API failed, check it's running");
        ////        Console.ForegroundColor = ConsoleColor.Gray;
        ////    }

        //}

        public bool Vector(string say)
        {
            // Added by CMChrisJones
            var sayAsByteArray = Encoding.UTF8.GetBytes(say);
            var encoded = System.Convert.ToBase64String(sayAsByteArray);

            try
            {
                using HttpClient client = new HttpClient();
                var result = client.GetAsync(VectorRestURL + "/say/" + encoded).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
