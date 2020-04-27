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
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
            var message = string.Format("{0} cuurent time is", DateTime.Now.ToShortTimeString());
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
