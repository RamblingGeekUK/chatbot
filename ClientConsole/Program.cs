using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ClientConsole
{
    class Program
    {
        static void Main()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44365/chathub")
                .Build();

            connection.StartAsync().Wait();
            connection.InvokeCoreAsync("SendMessage", args: new[] { "Wayne", "Hello" });
            connection.On("ReceiveMessage", (string userName, string message) =>
            {
                Console.WriteLine(userName + ':' + message);
            });

            connection.On("ReceiveTwitchMessage", (string message, string username) =>
            {
                Console.WriteLine(@"{0} / {1}", message, username);
            });

            Console.ReadKey();
        }
    }
}
