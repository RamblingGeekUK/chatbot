using TwitchLib.Client.Events;

namespace ChatBot
{
    public interface ICommand
    {
        void ExecuteAsync(OnChatCommandReceivedArgs e);
    }
}
