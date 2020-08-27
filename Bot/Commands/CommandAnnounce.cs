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
                await robot.GrantApiAccessAsync(Settings.Vector_Name, Settings.Vector_IP, Settings.Vector_Serial, Settings.Vector_Username, Settings.Vector_Password);
                await robot.ConnectAsync(Settings.Vector_Name);

                BatteryState x = await robot.GetBatteryStateAsync();

                //if (!x.IsCharging)
                //{
                //    //gain control over the robot by suppressing its personality
                    robot.StartSuppressingPersonality();
                    await robot.WaitTillPersonalitySuppressedAsync();
                              

                    //say something
                    await robot.Audio.SetMasterVolumeAsync(5);
                    await robot.Audio.SayTextAsync(message);
                
                    robot.StopSuppressingPersonality();

                    await robot.DisconnectAsync();
                    return true;
                //}
                //else
                //{
                //    Helpers.StatusInfo($"Vector is charging", "fail");
                //    return false;
                //}
                
            }
            catch (Exception e)
            {
                Log.Information($"Connecting to Vector failed! {e.Message}", "fail");

                return false;
            }
        }
    }
}
