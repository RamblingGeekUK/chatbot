using Serilog;
using System;
using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandWhisper : CommandBase, ICommand
    {
        public CommandWhisper(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            try
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                client.SendMessage(e.Command.ChatMessage.Channel, "rambli4Vector Sending..");
                new CommandAnnounce(client).Execute(message, e);

                Thread.Sleep(10);
                Log.Information($"[Delayed 10 seconds] {message}", "vector");
            }
            catch (Exception)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "Looks like you didn't give vector anything to say, try again.");
                Log.Information($"Vector was asked to say something but no text was given", "fail");
            }
          
        }
    }
}
