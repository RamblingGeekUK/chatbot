using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandLocalTime : CommandBase, ICommand
    {
        public CommandLocalTime(TwitchClient client)
            : base(client)
        {
        }

    public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            switch(message)
            {

                case "local":
                    client.SendMessage(e.Command.ChatMessage.Channel, "local");
                    Helpers.StatusInfo($"local", "fail");
                    var msg = string.Format("{0} curent time is", DateTime.Now.ToShortTimeString());
                    new CommandAnnounce(client).Execute(msg, e);
                    break;
                case "streamer":
                    // client.SendMessage(e.Command.ChatMessage.Channel, "local");
                    // Helpers.StatusInfo($"local", "fail");
                    // var msg = string.Format("{0} curent time is", DateTime.Now.ToShortTimeString());
                    // new CommandAnnounce(client).Execute(msg, e);
                    break;
                case "uptime":
                    //client.SendMessage(e.Command.ChatMessage.Channel, "uptime");
                    Helpers.StatusInfo($"uptime", "fail");
                    break;
                default:
                    //client.SendMessage(e.Command.ChatMessage.Channel, "default");
                    Helpers.StatusInfo($"default", "fail");
                    break;
            }
        }
    }
}
