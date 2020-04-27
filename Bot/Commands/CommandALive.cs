using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandALive : CommandBase, ICommand
    {
        public CommandALive(TwitchClient client)
            : base(client)
        {
        }

        public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {
            this.MessageChat(e.Command.ChatMessage.Channel, " is ALIVE!");
        }
    }
}
