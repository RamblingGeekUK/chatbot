using Serilog;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandTime : CommandBase, ICommand
    {
        public CommandTime(TwitchClient client)
            : base(client)
        {
        }

    public void Execute(OnChatCommandReceivedArgs e)
        {

            if (e.Command.ChatMessage.Message.Length > 12)
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

                switch (message)
                {
                    case "local":
                        // Not Implemented 
                        Log.Information($"local", "ok");
                        break;
                    case "streamer":
                        // Not Implemented
                        Log.Information($"streamer", "ok");
                        break;
                    case "up":
                    case "uptime":
                        // Not Implemented
                        Log.Information($"up or uptime", "ok");
                        break;
                }
            }
            else
            {
                Log.Information($"local/default", "ok");
                var msg = string.Format("{0} curent time is", DateTime.Now.ToShortTimeString());
                new CommandAnnounce(client).Execute(msg, e);
            }
        }
    }
}
