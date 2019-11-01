using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TwitchLib.Client;

namespace ChatBot.Base
{
    public abstract class CommandBase
    {
        protected readonly TwitchClient client;
        //protected readonly TwitchPubSub clientpubsub;
        protected readonly string VectorRestURL = "http://localhost:5000";
        

        public CommandBase(TwitchClient client)
        {
            this.client = client;
        }

        //public CommandBase(TwitchPubSub clientpubsub)
        //{
        //    this.clientpubsub = clientpubsub;
        //}

        protected void MessageChat(string channel, string message)
        {
            Console.WriteLine($"CommandBase MessageChat : {message}");
            this.client.SendMessage(channel, message);
        }

        protected void VectorFreePlayPost(Boolean enable)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Console.WriteLine("Calling API setFreeplayEnabled...");

                    //FreePlay freeplaycommand = new FreePlay();
                    //string json = JsonConvert.SerializeObject(freeplaycommand);
                    var content = new StringContent("", Encoding.UTF8, "application/json");

                    if (enable == true)
                    {
                        var result = client.PostAsync(VectorRestURL + "/setFreeplayOn", content).Result;
                        Console.WriteLine("Free play return status: {0}", result.StatusCode);
                    }
                    else
                    {
                        var result = client.PostAsync(VectorRestURL + "/setFreeplayOff", content).Result;
                        Console.WriteLine("Free play return status: {0}", result.StatusCode);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Call failed, check the Vector API is running");

            }
        }

        //internal class FreePlay
        //{
        //    public Boolean isFreeplayEnabled { get; set; }
        //}
    }
}