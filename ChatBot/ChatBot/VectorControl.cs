using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace ChatBot
{
    public class VectorControl
    {
        //connect 
        Robot robot = new Robot();
        public VectorControl()
        {
            _ = Connect();
        }
        public async Task Connect()
        {
           // await robot.GrantApiAccessAsync("Vector-N6T3", "192.168.1.7", "00403161", "wayne@kryptos.co.uk", "n&xAr1eCqbR5a^i8K#d2");

            await robot.ConnectAsync("Vector-N6T3"); //example: M9W4

            //gain control over the robot by suppressing its personality
            robot.StartSuppressingPersonality();
            await robot.WaitTillPersonalitySuppressedAsync();

            Console.WriteLine(robot.GetBatteryStateAsync().Result.BatteryLevel.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.BatteryVolts.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.IsCharging.ToString());
            Console.WriteLine(robot.GetBatteryStateAsync().Result.IsOnChargerPlatform.ToString());

            //say something
            await robot.Audio.SayTextAsync("Hello t b d gamer");

            //disconnect
            await robot.DisconnectAsync();
        }

        public async Task VectorTalk(string message)
        {
            await robot.ConnectAsync("Vector-N6T3");

            //gain control over the robot by suppressing its personality
            robot.StartSuppressingPersonality();
            await robot.WaitTillPersonalitySuppressedAsync();

            //say something
            await robot.Audio.SayTextAsync(message);
            await robot.DisconnectAsync();
        }
    }
}
