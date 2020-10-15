using DSharpPlus.EventArgs;
using Serilog;
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

        public async void Execute(string message)
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

        internal void Execute(string v, MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Vector(string message)
        {
            try
            {
                Robot robot = new Robot();
                await robot.GrantApiAccessAsync(Settings.Vector_Name, Settings.Vector_IP, Settings.Vector_Serial, Settings.Vector_Username, Settings.Vector_Password);
                await robot.ConnectAsync(Settings.Vector_Name);

                BatteryState x = await robot.GetBatteryStateAsync();

             
                //gain control over the robot by suppressing its personality
                robot.StartSuppressingPersonality();
                await robot.WaitTillPersonalitySuppressedAsync();
                              
                //say something
                await robot.Audio.SetMasterVolumeAsync(5);
                await robot.Audio.SayTextAsync(message);
                robot.StopSuppressingPersonality();
                await robot.Audio.SetMasterVolumeAsync(1);
                await robot.DisconnectAsync();
                Log.Logger.Information("Vector should of spoke the the text!");
                return true;
            }
            catch (Exception e)
            {
                Log.Information($"Connecting to Vector failed! {e.Message}", "fail");

                return false;
            }
        }
    }
}
