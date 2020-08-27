using TwitchLib.Client;
using TwitchLib.Client.Events;
using ChatBot;

namespace ChatBot.Base
{
    public class CommandDiscord : CommandBase, ICommand
    {
        public CommandDiscord(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            this.MessageChat(e.Command.ChatMessage.Channel, " is ALIVE!");
            //await PostMessage(729021058568421386, "Hello World");
        }


    }
}
