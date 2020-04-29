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
                case "up":
                case "uptime":
                    //client.SendMessage(e.Command.ChatMessage.Channel, "uptime");

                    // TimeSpan? GetUptime()
                    // {
                    //     //string userid = GetUserID(client.Channel)
                    //     return client.GetUptime(e.Command.ChatMessage.Channel).Result;
                    // }

                    Helpers.StatusInfo($"uptime {0}", "fail");
                    break;
                default:
                    client.SendMessage(e.Command.ChatMessage.Channel, "Vaild commands are, local - your local time, streamer - the streamers local time, up/uptime - stream up time");
                    Helpers.StatusInfo($"default", "fail");
                    break;
            }
        }


    }
}
