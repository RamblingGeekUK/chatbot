using System;
using System.Diagnostics;
using TwitchLib.Client;
using TwitchLib.Client.Events;


namespace ChatBot.Base
{
    public class CommandScene : CommandBase, ICommand
    {
        public CommandScene(TwitchClient client)
            : base(client)
        {
        }

        public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            string message = e.Command.ChatMessage.Message;
            message = message[message.LastIndexOf(" ")..];
            Console.WriteLine("Output: " + message);
            string scene;
            switch (message.Trim())
            {
                case "1":
                    Console.WriteLine("Switing to Scene {0}", message);
                    scene = "[1] RG-CAM-01 // MainScreen";
                    Run_cmd(" /scene=", scene);
                    break;
                case "2":
                    scene = "[2] RG-CAM-01 // Chat";
                    Console.WriteLine("Switing to Scene {0}", message);
                    Run_cmd(" /scene=", scene);
                    break;
                case "3":
                    scene = "[3] RG-MULTI-CAM // Show and Tell";
                    Console.WriteLine("Switing to Scene {0}", message);
                    Run_cmd(" /scene=", scene);
                    break;
                default:
                    break;
            }
        }

        private void Run_cmd(string cmd, string args)
        {
            //string torun = "C:\\Users\\wtt\\Downloads\\OBSCommand_1.4.6\\OBSCommand\\OBSCommand.exe /scene=\"" + args + "\"";

           
            Process.Start("C:\\Users\\wtt\\Downloads\\OBSCommand_1.4.6\\OBSCommand\\OBSCommand.exe", cmd + "\"" + args + "\"");
            Console.WriteLine(args);
        }
    }
}
