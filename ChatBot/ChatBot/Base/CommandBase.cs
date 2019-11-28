using System;
using TwitchLib.Client;


namespace ChatBot.Base
{
    public abstract class CommandBase
    {
        protected readonly TwitchClient client;
        //protected readonly TwitchPubSub clientpubsub;
        protected readonly string VectorRestURL = "http://localhost:5000";
        
        public CommandBase(TwitchClient client)
        {
            this.client = client;
        }

        protected void MessageChat(string channel, string message)
        {

            //ChatBot.
            //Console.WriteLine($"CommandBase MessageChat : {message}");
            
            this.client.SendMessage(channel, message);
        }        
    }
}