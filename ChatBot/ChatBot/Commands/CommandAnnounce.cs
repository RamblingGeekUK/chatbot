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
                await robot.ConnectAsync("Vector-N6T3");
                await robot.Screen.SetEyeColor(Color.Azure);
                //await robot.Animation.PlayAsync("GreetAfterLongTime");

                //gain control over the robot by suppressing its personality
                robot.StartSuppressingPersonality();
                await robot.WaitTillPersonalitySuppressedAsync();

                //say something
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
