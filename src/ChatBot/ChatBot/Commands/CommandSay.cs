﻿using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandSay : CommandBase, ICommand
    {
        public CommandSay(TwitchClient client)
            : base(client)
        {
        }
        
        public void Execute(OnChatCommandReceivedArgs e)
        {
            try
            {
                string message = e.Command.ChatMessage.Message.Remove(0, e.Command.CommandText.Length + 2);
                Helpers.StatusInfo($"{message}", "vector");
                client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");
                new CommandAnnounce(client).Execute(message, e);
            }
            catch (Exception)
            {
                client.SendMessage(e.Command.ChatMessage.Channel, "Looks like you didn't give vector anything to say, try again.");
                Helpers.StatusInfo($"Vector was asked to say something but no text given", "fail");
            }
          
        }
    }
}