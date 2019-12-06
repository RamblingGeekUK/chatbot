using System;
using System.Threading.Tasks;
using Vector;

namespace VectorControl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //connect
            var robot = new Robot();
           
           // await robot.GrantApiAccessAsync("Vector-N6T3", "192.168.1.7", "00403161", "wayne@kryptos.co.uk", "n&xAr1eCqbR5a^i8K#d2");

            await robot.ConnectAsync("Vector-N6T3"); //example: M9W4

            //gain control over the robot by suppressing its personality
            robot.StartSuppressingPersonality();
            await robot.WaitTillPersonalitySuppressedAsync();


            Console.WriteLine(robot.GetBatteryStateAsync().Result.BatteryLevel.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.BatteryVolts.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.IsCharging.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.IsOnChargerPlatform.ToString());



            //drive off Charger
            //await robot.Motors.DriveOffChargerAsync();
            //await robot.Motors.DriveStraightAsync(50, 50);

            //drive in a square
            //await robot.Motors.DriveStraightAsync(50, 50);
            //await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
            //await robot.Motors.DriveStraightAsync(50, 50);
            //await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
            //await robot.Motors.DriveStraightAsync(50, 50);
            //await robot.Motors.TurnInPlaceAsync(1.5708f, 5);
            //await robot.Motors.DriveStraightAsync(50, 50);
            //await robot.Motors.TurnInPlaceAsync(1.5708f, 5);

            //play an animation
            //await robot.Animation.PlayAsync("anim_vc_laser_lookdown_01");
            await robot.Animation.PlayAsync("DanceBeatCantDoThat");

            //say something
            await robot.Audio.SayTextAsync("Hello t b d gamer");

            //disconnect
            await robot.DisconnectAsync();
        }

       
    }
}
