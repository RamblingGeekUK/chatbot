using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandAttention : CommandBase, ICommand
    {
        public CommandAttention(TwitchClient client)
            : base(client)
        {
        }

    public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
            var message = string.Format("{0} has asked you to stop and please check chat", e.Command.ChatMessage.Username);
            new CommandAnnounce(client).Execute(message, e);
        }
    }
}
