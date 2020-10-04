using Serilog;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandFdgt : CommandBase, ICommand
    {
        public CommandFdgt(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            try
            {
                //string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                //Log.Information($"{message}", "fdgt-test");
                client.SendMessage(e.Command.ChatMessage.Channel, "raid");

                //new CommandAnnounce(client).Execute(message, e);
            }
            catch (Exception)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "Looks like you didn't give vector anything to say, try again.");
                Log.Information($"Vector was asked to say something but no text was given", "fail");
            }
          
        }
    }
}
