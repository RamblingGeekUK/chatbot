using ChatBot.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;


namespace ChatBot
{

    public partial class Bot
    {
        private readonly string chatfilename = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + DateTime.UtcNow.ToString("dd-mm-yyyy--hh-mm-ss") + ".chat";
        private readonly string linkfilename = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + DateTime.UtcNow.ToString("dd-MM-yyyy--HH-mm-ss") + ".links";
        private readonly TwitchClient client;
        private readonly Dictionary<string, ICommand> commands;
        private readonly List<string> coders;

        public Boolean VectorAlive = false;

        public Bot()
        {
            try
            {
                ConnectionCredentials credentials = new ConnectionCredentials(Settings.Twitch_botusername, Settings.Twitch_token);

                this.client = new TwitchClient();
                this.client.Initialize(credentials, Settings.Twitch_channel);
                this.client.OnLog += Client_OnLog;
                this.client.OnMessageReceived += OnMessageReceived;
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
                    { "freeplay", new CommandFreePlay(client) },
                    { "commands", new CommandCommands(client) },
                    { "scene", new CommandScene(client) },
                };

                coders = GetLiveCoders();
            }
            catch (Exception)
            {
                System.Console.WriteLine(Settings.Twitch_botusername);
                System.Console.WriteLine(Settings.Twitch_token);
            }
        }

      
        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            Helpers.StatusInfo($"Whisper received from : {e.WhisperMessage.DisplayName}", "ok");
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.IsBroadcaster)
            {
                //string message = "hey, don't forget to follow and subscribe, if you're a twitch prime member, drop your free sub here.";
                //new CommandAnnounce(client).Execute(message, e);
            }
            else if (coders.Contains(e.ChatMessage.DisplayName))
            {
                _ = ("!so " + e.ChatMessage.DisplayName);
                new CommandAnnounce(client).Execute($"A live coder is in the chat, check out {e.ChatMessage.DisplayName}, stream at twitch.tv/{e.ChatMessage.DisplayName}", e);
                coders.Remove(e.ChatMessage.DisplayName);
            }

            StreamWriter writer;

            foreach (Match link in Regex.Matches(e.ChatMessage.Message, @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})"))
            {
                if (File.Exists(linkfilename) == true)
                {
                    using (writer = File.AppendText(linkfilename))
                    {
                        Helpers.StatusInfo($"link : {DateTime.UtcNow.ToString()}, {link.Value}","info");
                        writer.WriteAsync($"link : {DateTime.UtcNow.ToString()}, {link.Value}" + Environment.NewLine);
                    }
                }
                else
                {
                    using (writer = File.CreateText(linkfilename))
                    {
                        Helpers.StatusInfo($"link : {DateTime.UtcNow.ToString()}, {link.Value}", "info");
                        writer.WriteAsync($"link : {DateTime.UtcNow.ToString()}, {link.Value}" + Environment.NewLine);
                    }
                }
            }


            if (File.Exists(chatfilename) == true)
            {
                using (writer = File.AppendText(chatfilename))
                {
                    writer.WriteAsync($"{DateTime.UtcNow.ToString()},{e.ChatMessage.UserType},{e.ChatMessage.DisplayName},{e.ChatMessage.Username},{e.ChatMessage.IsSubscriber.ToString()},{e.ChatMessage.Message}" + Environment.NewLine);
                }
            }
            else
            {
                using (writer = File.CreateText(chatfilename))
                {
                    writer.WriteAsync($"{DateTime.UtcNow.ToString()},{e.ChatMessage.UserType},{e.ChatMessage.DisplayName},{e.ChatMessage.Username},{e.ChatMessage.IsSubscriber.ToString()},{e.ChatMessage.Message}" + Environment.NewLine);
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
            //Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }
        private void Client_OnConnectedAsync(object sender, OnConnectedArgs e)
        {
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

            Helpers.StatusInfo($"Assembly Version {GetType().Assembly.GetName().Version.ToString()}", "info");
            
            foreach(var item in heserver.AddressList)
            {
                Helpers.StatusInfo($"IP Address {item.ToString()}", "info");
            }
            Helpers.StatusInfo($"Connected to Channel ({e.AutoJoinChannel})", "ok");
        }
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            string vtalktext = "Hello World! Vector is Alive!";
           
            Helpers.StatusInfo("Bot Joined Chat", "ok");
            new CommandAnnounce(client).Execute(vtalktext, e);
            Helpers.StatusInfo($"{vtalktext}", "vector");

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