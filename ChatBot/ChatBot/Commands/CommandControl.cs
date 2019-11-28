using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandControl : CommandBase, ICommand
    {
        public CommandControl(TwitchClient client)
            : base(client)
        {
        }

        public async void Execute(string message, OnChatCommandReceivedArgs e)
        {
            //this.MessageChat(e.Command.ChatMessage.BotUsername, message);
            await this.Vector();
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Vector()
        {
            try
            {
                Robot robot = new Robot();
                // Secrets live here for the moment - do not show. 
                await robot.GrantApiAccessAsync("Vector-N6T3", "192.168.1.7", "00403161", "wayne@kryptos.co.uk", "n&xAr1eCqbR5a^i8K#d2");
                await robot.ConnectAsync("Vector-N6T3");

                //gain control over the robot by suppressing its personality
                robot.StartSuppressingPersonality();
                await robot.WaitTillPersonalitySuppressedAsync();

                // drive off Charger
                await robot.Motors.DriveOffChargerAsync();
                await robot.Motors.DriveStraightAsync(50, 50);

                //drive in a square
                await robot.Motors.DriveStraightAsync(50, 50);
                await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
                await robot.Motors.DriveStraightAsync(50, 50);
                await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
                await robot.Motors.DriveStraightAsync(50, 50);
                await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
                await robot.Motors.DriveStraightAsync(50, 50);
                await robot.Motors.TurnInPlaceAsync(1.5708f, 5);

                //play an animation
                await robot.Animation.PlayAsync("anim_vc_laser_lookdown_01");

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
