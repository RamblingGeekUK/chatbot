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
            this.SendMessage(e.Channel, message);
            this.Vector(message);
        }
        public void Execute(string message, OnMessageReceivedArgs e)
        {
            this.SendMessage(e.ChatMessage.Channel, message);
            this.Vector(message);
        }

        public void Execute(string message, OnChatCommandReceivedArgs e)
        {
            this.SendMessage(e.Command.ChatMessage.BotUsername, message);
            this.Vector(message);
        }

        public void Vector(string say)
        {
            // Added by CMChrisJones
            var sayAsByteArray = Encoding.UTF8.GetBytes(say);
            var encoded = System.Convert.ToBase64String(sayAsByteArray);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Console.WriteLine("Calling API ..." + encoded);
                    var result = client.GetAsync(VectorRestURL + "/say/" + encoded).Result;
                }
            }
            catch
            {
                Console.WriteLine("Called failed, check the Vector API is running");
            }
        }

    }
}
