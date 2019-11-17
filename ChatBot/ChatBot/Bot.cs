using ChatBot.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using Vector;

namespace ChatBot
{
    
    public class Bot
    {
        private readonly TwitchClient client;
        private readonly Dictionary<string, ICommand> commands;
        // Create filename based on todays date and time to be used to log chat to text file
        private readonly string chatfilename = "D:\\OneDrive\\Stream\\ChatLogs\\" + DateTime.UtcNow.ToString("dd-MM-yyyy--HH-mm-ss") + ".chat";
        // Create filename based on todays date and time to be used to log links to a text file
        private readonly string linkfilename = "D:\\OneDrive\\Stream\\ChatLogs\\" + DateTime.UtcNow.ToString("dd-MM-yyyy--HH-mm-ss") + ".links";
        private readonly List<string> coders;
        //private readonly List<string> Links = new List<string>();

        public Boolean VectorAlive = false;

        public Bot()
        {
            // Wait to ensure the VectorREST API has had time to start. 
            System.Threading.Thread.Sleep(5000);

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
                { "vector-joke", new CommandTellJoke(client) },
                { "attention", new CommandAttention(client) },
                { "lurk", new CommandLurk(client) },
                { "unlurk", new CommandUnLurk(client) },
                { "freeplay", new CommandFreePlay(client) },
                { "commands", new CommandCommands(client) },
                { "scene", new CommandScene(client) },
            };

            coders = GetLiveCoders();
            Speak("If this works it will be amazing");
        }

        private async void Speak(string message)
        {
            Robot robot = new Robot();
            await robot.ConnectAsync("Vector-N6T3");

            //gain control over the robot by suppressing its personality
            robot.StartSuppressingPersonality();
            await robot.WaitTillPersonalitySuppressedAsync();

            //say something
            await robot.Audio.SayTextAsync(message);
            await robot.DisconnectAsync();
            
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Whisper received from {0}", $" : {e.WhisperMessage.DisplayName}".PadLeft(30, '.'));
            Console.ForegroundColor = ConsoleColor.Gray;
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
                        Console.WriteLine($"link : {DateTime.UtcNow.ToString()}, {link.Value}");
                        writer.WriteAsync($"link : {DateTime.UtcNow.ToString()}, {link.Value}" + Environment.NewLine);
                    }
                }
                else
                {
                    using (writer = File.CreateText(linkfilename))
                    {
                        Console.WriteLine($"link : {DateTime.UtcNow.ToString()}, {link.Value}");
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Connected to Channel ({e.AutoJoinChannel}) : OK");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot Joined Chat {0}", " : OK".PadLeft(24,'.'));            
            Console.ForegroundColor = ConsoleColor.Gray;

            if (new CommandAnnounce(client).Vector(""))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Call to Vector API {0}", " : OK".PadLeft(21,'.'));
                Console.ForegroundColor = ConsoleColor.Gray;

                new CommandAnnounce(client).Vector("Hello World!");
                VectorAlive = true;
            }
            else
            {
                VectorAlive = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call to Vector API : {0} ", " : Failed".PadLeft(23,'.'));
                Console.ForegroundColor = ConsoleColor.Gray;
            }

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
                List<TwitchLiveCoders> coders = new List<TwitchLiveCoders>();
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