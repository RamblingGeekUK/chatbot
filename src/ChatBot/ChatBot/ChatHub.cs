using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string userName, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", userName, message);
        //}

        public async Task SendMessage(string message, string username)
        {
            await Clients.All.SendAsync("ReceiveTwitchMessage", message, username);
        }

        //public static void Message(string message)

      
    }
}
