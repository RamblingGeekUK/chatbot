using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;



namespace ChatBot.Base
{
    public class CommandPlay : CommandBase, ICommand
    {
        public CommandPlay(TwitchClient client)
            : base(client)
        {
        }
        
        public void ExecuteAsync(OnChatCommandReceivedArgs e)
        {

            throw new NotImplementedException();
        }
    }
}
