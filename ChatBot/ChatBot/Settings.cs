using System;

namespace ChatBot
{
    public static class Settings
    {
        public static string Twitch_channel = Environment.GetEnvironmentVariable("Twitch_channel");
        public static string Twitch_botusername = Environment.GetEnvironmentVariable("Twitch_botusername");
        public static string Twitch_token = Environment.GetEnvironmentVariable("Twitch_token");
        public static string Twitch_ID = Environment.GetEnvironmentVariable("Twitch_ID");
        public static string Twitch_OAuth = Environment.GetEnvironmentVariable("Twitch_OAuth");

        //("Vector-N6T3", "192.168.1.7", "00403161", "wayne@kryptos.co.uk", "n&xAr1eCqbR5a^i8K#d2");

        public static string Vector_Name = Environment.GetEnvironmentVariable("Vector_Name");
        public static string Vector_IP = Environment.GetEnvironmentVariable("Vector_IP");
        public static string Vector_Serial = Environment.GetEnvironmentVariable("Vector_Serial");
        public static string Vector_Username = Environment.GetEnvironmentVariable("Vector_Username");
        public static string Vector_Password = Environment.GetEnvironmentVariable("Vector_Password");

    }
}