using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;
using System.Threading.Tasks;

namespace ChatBot.Base
{
    public class CommandMove : CommandBase, ICommand
    {
        public CommandMove(TwitchClient client)
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

                // 
                robot.StopSuppressingPersonality();

                //disconnect
                await robot.DisconnectAsync();
                return true;
            }
            catch (Exception)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "Looks like you didn't give vector anything to say, try again.");
                Helpers.StatusInfo($"Vector was asked to say something but no text was given", "fail");
                return false;
            }
        }
    }
}
