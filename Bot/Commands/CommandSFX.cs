using Serilog;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandSFX : CommandBase, ICommand
    {
        public CommandSFX(TwitchClient client)
            : base(client)
        {
        }
        public void Execute(OnChatCommandReceivedArgs e)
        {


            //Robot robot = new Robot();
            //await robot.GrantApiAccessAsync(Settings.Vector_Name, Settings.Vector_IP, Settings.Vector_Serial, Settings.Vector_Username, Settings.Vector_Password);
            //await robot.ConnectAsync(Settings.Vector_Name);

            ////gain control over the robot by suppressing its personality
            //robot.StartSuppressingPersonality();
            //await robot.WaitTillPersonalitySuppressedAsync();

          

            if (e.Command.ChatMessage.Message.Length > 3)
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

                switch (message.ToLower())
                {
                    case "raid":
                        
                        client.SendMessage(e.Command.ChatMessage.Channel, "RAID");
                        PlaySFX(message.ToLower());
                        break;
                    case "boo":
                        client.SendMessage(e.Command.ChatMessage.Channel, "boo");
                        break;
                    case "stop":
                      
                        break;
                }
            }
        }
    }
}
