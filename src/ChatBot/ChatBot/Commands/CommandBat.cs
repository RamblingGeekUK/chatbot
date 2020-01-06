using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandBat : CommandBase, ICommand
    {
        public CommandBat(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
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

                BatteryState x = await robot.GetBatteryStateAsync();

                // Vector is considered fully - charged above 4.1 volts.At 3.6V, the robot is approaching low charge.
                // Battery_level values are as follows:
                // Low = 1: 3.6V or less.If on charger, 4V or less.
                // Nominal = 2
                // Full = 3: only be achieved when Vector is on the charger.

                client.SendMessage(e.Command.ChatMessage.Channel, $"Battery State : {x.BatteryLevel.ToString()}, On Platform : {x.IsOnChargerPlatform.ToString()}, Voltage : {x.BatteryVolts.ToString()}, Charging : {x.IsCharging.ToString()}");
                //say something
                await robot.Audio.SayTextAsync("all done");

                //disconnect
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
