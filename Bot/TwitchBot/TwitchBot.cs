﻿using ChatBot.Base;
using ChatBot.Fauna;
using ChatBot.Helpers;
using FaunaDB.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;



namespace ChatBot
{
    public class TwitchBot : ITwitchBotService
    {
        private readonly string streampost = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + DateTime.UtcNow.ToString("yyyy-dd-MM-hh-mm-ss") + ".md";
        private TwitchClient client;
        private Dictionary<string, ICommand> commands;
        private List<string> coders;

        static readonly string ENDPOINT = "https://db.fauna.com:443";
        public Task Start()
        {

            try
            {
                var credentials = new ConnectionCredentials(Settings.Twitch_botusername, Settings.Twitch_token);

                client = new TwitchClient();
                client.Initialize(credentials, Settings.Twitch_channel);
                client.OnLog += Client_OnLog;
                client.OnMessageReceived += OnMessageReceived;
                client.OnJoinedChannel += Client_OnJoinedChannel;
                client.OnConnected += Client_OnConnectedAsync;
                client.OnChatCommandReceived += Client_OnChatCommandReceived;
                client.OnRaidNotification += Client_OnRaidNotification;
                client.OnWhisperReceived += Client_OnWhisperReceived;
                client.Connect();

                this.commands = CommandHelper.GetCommands(client);

                coders = GetLiveCoders();  // this fails :D
            }
            catch (Exception)
            {
                Log.Logger.Error("Normally to end up here, no enviroment varaibles are set 😊");
                System.Environment.Exit(0);
            }

            return Task.FromResult(0);
        }

      
        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            Log.Information($"Whisper received from : {e.WhisperMessage.DisplayName}", "ok");
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {



            var fclient = new FaunaClient(endpoint: ENDPOINT, secret: Settings.Fauna_Secret);

            if (coders.Contains(e.ChatMessage.DisplayName))
            {
                //string name = await GetVectorPronunciation(fclient, e.ChatMessage.DisplayName);
                _ = ("!so " + e.ChatMessage.DisplayName);
                new CommandAnnounce(client).Execute($"A live coder is in the chat, check out {e.ChatMessage.DisplayName}, stream at twitch dot tv/{e.ChatMessage.DisplayName}", e);
                coders.Remove(e.ChatMessage.DisplayName);
            }

            foreach (Match link in Regex.Matches(e.ChatMessage.Message,
                @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})"))
            {
                var protocollink = new UriBuilder(link.Value.ToString()).Uri.ToString();
              
                if (!e.ChatMessage.DisplayName.StartsWith("StreamElements"))
                {
                //    Log.Information($"link : {DateTime.UtcNow.ToString()}, {protocollink}", "info");
                //    Data.WriteLink(fclient, protocollink).Wait();
                    DiscordBot.PostMessage(e.ChatMessage.DisplayName, 729021058568421386, protocollink).Wait();
                }
            }

            //var connection = new HubConnectionBuilder()
            //   .WithUrl("http://localhost:53425/chathub")
            //   .Build();

            //connection.StartAsync().Wait();
            //connection.InvokeCoreAsync("SendMessage", args: new[] { e.ChatMessage.Message, e.ChatMessage.Username });


            //Data.GetVectorPronunciation(fclient, e.ChatMessage.Username).Wait();
            // Data.GetVectorPronunciationAll(fclient).Wait();
        }

        private void Client_OnRaidNotification(object sender, OnRaidNotificationArgs e)
        {
            // Say thank you for the raid 3 times.
            for (int i = 0; i < 3; i++)
            {
                new CommandAnnounce(client).Execute("Thank you for the raid!", e);
                new CommandAnnounce(client).Execute("Join the discord channel", e);
                new CommandAnnounce(client).Execute("!discord", e);
            }

        }
        private void Client_OnLog(object sender, OnLogArgs e)
        {
            
        }
        private void Client_OnConnectedAsync(object sender, OnConnectedArgs e)
        {
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());        
            foreach(var item in heserver.AddressList)
            {
                Log.Information($"Local IP Address : {item.ToString()}", "info");
            }
          
            Log.Information($"Connected to Twitch Channel : ({e.AutoJoinChannel})", "ok");
            Log.Information($"Vector IP : {Settings.Vector_IP}", "info");
            Log.Information("Twitch Bot Started");
        }
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            string vtalktext = "Hi, I'm here!";

            Log.Information("TwitchBot Joined Twitch Chat", "ok");
            client.SendMessage(e.Channel, "rambli4Hype");
            new CommandAnnounce(client).Execute(vtalktext, e);
            Log.Information($"{vtalktext}", "vector");

        }
        private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
        {
            if (this.commands.ContainsKey(e.Command.CommandText.ToLower()) == false)
                return;

            this.commands[e.Command.CommandText.ToLower()].Execute(e);
        }

        public List<string> GetLiveCoders()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                using HttpClient client = httpClient;
                string url = "https://api.twitch.tv/kraken/teams/livecoders";

                client.DefaultRequestHeaders.Add("Client-ID", Settings.Twitch_ID);
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");

                var result = client.GetStringAsync(url).Result;
                //List<TwitchLiveCoders> coders = new List<TwitchLiveCoders>();
                var team = JsonConvert.DeserializeObject<TwitchLiveCoders>(result);

                return new List<string>(team.users.Select(c => c.display_name));
            }
            catch
            {
                Console.WriteLine("Call failed");
                return new List<string>();
            }
        }



    }
}