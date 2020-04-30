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

    public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {

            if (e.Command.ChatMessage.Message.Length > 12)
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

                switch (message)
                {
                    case "local":
                        // Not Implemented 
                        Helpers.StatusInfo($"local", "ok");
                        break;
                    case "streamer":
                        // Not Implemented
                        Helpers.StatusInfo($"streamer", "ok");
                        break;
                    case "up":
                    case "uptime":
                        // Not Implemented
                        Helpers.StatusInfo($"up or uptime", "ok");
                        break;
                }
            }
            else
            {
                Helpers.StatusInfo($"local/default", "ok");
                var msg = string.Format("{0} curent time is", DateTime.Now.ToShortTimeString());
                new CommandAnnounce(client).Execute(msg, e);
            }
        }
    }
}
