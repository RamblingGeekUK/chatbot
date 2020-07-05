using System;

namespace ChatBot
{
    public class Settings
    {
        public string Twitch_channel;


        Twitch_channel = "Hello World"; //Environment.GetEnvironmentVariable("Twitch_channel");


        public static string Twitch_botusername = Environment.GetEnvironmentVariable("Twitch_botusername");
        public static string Twitch_token = Environment.GetEnvironmentVariable("Twitch_token");
        public static string Twitch_ID = Environment.GetEnvironmentVariable("Twitch_ID");
        public static string Twitch_OAuth = Environment.GetEnvironmentVariable("Twitch_OAuth");

        public static string Vector_Name = Environment.GetEnvironmentVariable("Vector_Name");
        public static string Vector_IP = Environment.GetEnvironmentVariable("Vector_IP");
        public static string Vector_Serial = Environment.GetEnvironmentVariable("Vector_Serial");
        public static string Vector_Username = Environment.GetEnvironmentVariable("Vector_Username");
        public static string Vector_Password = Environment.GetEnvironmentVariable("Vector_Password");

    }
}