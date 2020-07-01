using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandVol : CommandBase, ICommand
    {
        public CommandVol(TwitchClient client)
            : base(client)
        {
        }

        public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            _ = Vector(e);
        }

        public async Task<bool> Vector(OnChatCommandReceivedArgs e)
        {
            try
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
                        case "low":
                            Helpers.StatusInfo($"Vol:low", "ok");
                            await robot.Audio.SetMasterVolumeAsync(1);  //lowest volume setting (1-5)
                            await robot.Audio.SayTextAsync($"Volume set to low");
                            robot.StopSuppressingPersonality();
                            //disconnect
                            await robot.DisconnectAsync();
                            break;
                        case "med":
                            Helpers.StatusInfo($"Vol:med", "ok");
                            await robot.Audio.SetMasterVolumeAsync(3);
                            //say something
                            await robot.Audio.SayTextAsync($"Volume set to medium");
                            robot.StopSuppressingPersonality();
                            //disconnect
                            await robot.DisconnectAsync();
                            break;
                        case "high":
                            Helpers.StatusInfo($"Vol:high", "ok");
                            await robot.Audio.SetMasterVolumeAsync(5);
                            //say something
                            await robot.Audio.SayTextAsync("Volume set to high",1,false);
                            robot.StopSuppressingPersonality();
                            //disconnect
                            await robot.DisconnectAsync();
                            break;
                    }
                }

               
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
