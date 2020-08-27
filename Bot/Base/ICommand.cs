using TwitchLib.Client.Events;

namespace ChatBot
{
    public interface ICommand
    {
        void Execute(OnChatCommandReceivedArgs e);
    }
}
