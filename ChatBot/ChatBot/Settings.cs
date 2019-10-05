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
        
    }
}