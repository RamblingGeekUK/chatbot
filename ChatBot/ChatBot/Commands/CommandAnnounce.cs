using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandAnnounce : CommandBase
    {
        public CommandAnnounce(TwitchClient client)
            : base(client)
        {
        }

        public async void Execute (string message, OnRaidNotificationArgs e)
        {
            //this.MessageChat(e.Channel, message);
            await this.Vector(message);
        }
        public async void Execute(string message, OnWhisperReceivedArgs e)
        {
            //this.MessageChat(e.WhisperMessage.Message, message);
            await this.Vector(message);
        }
        public async void Execute(string message, OnMessageReceivedArgs e)
        {
            //this.MessageChat(e.ChatMessage.Channel, message);
            await this.Vector(message);
        }

        public async void Execute(string message, OnChatCommandReceivedArgs e)
        {
            //this.MessageChat(e.Command.ChatMessage.BotUsername, message);
            await this.Vector(message);
        }

        public async void Execute(string message, OnJoinedChannelArgs e)
        {
            //this.MessageChat(e.Channel, message);
            await this.Vector(message);
        }
             
        public async Task<bool> Vector(string message)
        {
            try
            {
                Robot robot = new Robot();
                await robot.GrantApiAccessAsync("Vector-N6T3", "192.168.1.7", "00403161", "wayne@kryptos.co.uk", "n&xAr1eCqbR5a^i8K#d2");
                await robot.ConnectAsync("Vector-N6T3");

                //gain control over the robot by suppressing its personality
                robot.StartSuppressingPersonality();
                await robot.WaitTillPersonalitySuppressedAsync();

                //say something
                await robot.Screen.SetScreenImage("https://static-cdn.jtvnw.net/jtv_user_pictures/e0b2472c-b103-44d3-b132-c618032217ef-profile_image-70x70.png");
                await robot.Audio.SayTextAsync(message);
                await robot.DisconnectAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
