using System;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandPlay : CommandBase, ICommand
    {
        public CommandPlay(TwitchClient client)
            : base(client)
        {
        }
        public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            _ = Vector(e);
        
        }
        public async Task<bool> Vector(OnChatCommandReceivedArgs e)
        {

            Robot robot = new Robot();
            await robot.GrantApiAccessAsync(Settings.Vector_Name, Settings.Vector_IP, Settings.Vector_Serial, Settings.Vector_Username, Settings.Vector_Password);
            await robot.ConnectAsync(Settings.Vector_Name);

            //gain control over the robot by suppressing its personality
            robot.StartSuppressingPersonality();
            await robot.WaitTillPersonalitySuppressedAsync();

            if (e.Command.ChatMessage.Message.Length > 12)
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

                switch (message)
                {
                    case "start":
                        Helpers.StatusInfo($"Play : Start", "ok");
                        await robot.Audio.SayTextAsync($"YES!, it's play time");
                        robot.StopSuppressingPersonality();
                        //disconnect
                        //await robot.DisconnectAsync();
                        break;
                    case "stop":
                        Helpers.StatusInfo($"Play : Stop", "ok");
                        await robot.Audio.SetMasterVolumeAsync(3);
                        //say something
                        await robot.Audio.SayTextAsync($"Yes, waiting for you to tell me what to do!");
                        robot.StartSuppressingPersonality();
                        //disconnect
                        //await robot.DisconnectAsync();
                        break;
                }
            }

            return true;
        }
    }
}
