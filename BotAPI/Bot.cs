using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace BotAPI
{
    public class Bot
    {
        private readonly TwitchClient client;
        //private readonly List<string> coders;

        public Bot()
            {

            try
            {
                ConnectionCredentials credentials = new ConnectionCredentials(Settings.Twitch_botusername, Settings.Twitch_token);

                this.client = new TwitchClient();
                this.client.Initialize(credentials, Settings.Twitch_channel);            
                this.client.OnMessageReceived += OnMessageReceived;
                this.client.Connect();
            }
            catch (Exception)
            {

                throw;
            }
            }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
