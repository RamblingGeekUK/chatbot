using ChatBot.Base;
using ChatBot.Fauna;
using FaunaDB.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
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
                
                this.client = new TwitchClient();
                this.client.Initialize(credentials, Settings.Twitch_channel);
                this.client.OnLog += Client_OnLog;
                this.client.OnMessageReceived += OnMessageReceivedAsync;
                this.client.OnJoinedChannel += Client_OnJoinedChannel;
                this.client.OnConnected += Client_OnConnectedAsync;
                this.client.OnChatCommandReceived += Client_OnChatCommandReceived;
                this.client.OnRaidNotification += Client_OnRaidNotification;
                this.client.OnWhisperReceived += Client_OnWhisperReceived;
                this.client.Connect();           

                this.commands = new Dictionary<string, ICommand>
                {
                    { "alive", new CommandALive(client) },
                    { "vector-say", new CommandSay(client) },
                    { "vs", new CommandSay(client) },
                    { "vector-joke", new CommandTellJoke(client) },
                    { "attention", new CommandAttention(client) },
                    { "lurk", new CommandLurk(client) },
                    { "unlurk", new CommandUnLurk(client) },
                    { "commands", new CommandCommands(client) },
                    { "scene", new CommandScene(client) },
                    { "vector-bat", new CommandBat(client) },
                    { "vector-vol", new CommandVol(client) },
                    { "vector-move", new CommandMove(client) },
                    { "vector-time", new CommandTime(client) },
                    { "vector-play", new CommandPlay(client) },
                    { "vector-whisper", new CommandWhisper(client) }
                };

                coders = GetLiveCoders();
            }
            catch (Exception)
            {
                Log.Logger.Error("Normally to end up here, no enviroment varaibles are set :-)");
            }

            return Task.FromResult(0);
        }

      
        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            Log.Information($"Whisper received from : {e.WhisperMessage.DisplayName}", "ok");
        }

        private void OnMessageReceivedAsync(object sender, OnMessageReceivedArgs e)
        {

         

            if (e.ChatMessage.IsBroadcaster)
            {
                //string message = "hey, don't forget to follow and subscribe, if you're a twitch prime member, drop your free sub here.";
                //new CommandAnnounce(client).Execute(message, e);
                //var connection = new HubConnectionBuilder()
                //    .WithUrl("https://localhost:44365/chathub")
                //    .Build();
                //connection.StartAsync().Wait();
                // connection.InvokeCoreAsync("SendMessage", args: new[] { e.ChatMessage.Message, e.ChatMessage.Username });

            }
            else if (coders.Contains(e.ChatMessage.DisplayName))
            {
                _ = ("!so " + e.ChatMessage.DisplayName);
                new CommandAnnounce(client).Execute($"A live coder is in the chat, check out {e.ChatMessage.DisplayName}, stream at twitch.tv/{e.ChatMessage.DisplayName}", e);
                coders.Remove(e.ChatMessage.DisplayName);

         
            }

            foreach (Match link in Regex.Matches(e.ChatMessage.Message,
                @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})"))
            {

                var protocollink = new UriBuilder(link.Value.ToString()).Uri.ToString();

                if (!e.ChatMessage.DisplayName.StartsWith("StreamElements"))
                {
                    Log.Information($"link : {DateTime.UtcNow.ToString()}, {protocollink}", "info");
                    var client = new FaunaClient(endpoint: ENDPOINT, secret: Settings.Fauna_Secret);
                    Data.WriteLink(client, protocollink).Wait();
                    DiscordBot.PostMessage(e.ChatMessage.DisplayName, 729021058568421386, protocollink).Wait();
                }
            }
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

            Log.Information("TwitchBot (J5Bot) Joined Twitch Chat", "ok");
            client.SendMessage(e.Channel, "TwitchBot (J5Bot) Joined Twitch Chat");
            client.SendMessage(e.Channel, "rambli4Vector is here.");
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