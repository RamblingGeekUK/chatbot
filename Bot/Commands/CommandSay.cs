using DSharpPlus.CommandsNext.Attributes;
using Serilog;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using Vector;

namespace ChatBot.Base
{
    public class CommandSay : CommandBase, ICommand
    {
        
        public CommandSay(TwitchClient client)
            : base(client)
        {
        }

        [Command("vector-say")]
        public void Execute(OnChatCommandReceivedArgs e)
        {
            try
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                Log.Information($"{message}", "vector");
                client.SendMessage(e.Command.ChatMessage.Channel, "rambli4Vector Sending..");
                new CommandAnnounce(client).Execute(message, e);
            }
            catch (Exception ex)
            { 
                Log.Information(ex.Message);
                Log.Information($"Vector was asked to say something but no text was given", "fail");
                client.SendMessage(e.Command.ChatMessage.Channel, "Looks like you didn't give vector anything to say, try again.");
            }
          
        }
    }
}
