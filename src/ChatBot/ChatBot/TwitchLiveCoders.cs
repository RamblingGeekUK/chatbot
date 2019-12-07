﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot
{
    public class TwitchLiveCoders
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string info { get; set; }
        public string display_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string logo { get; set; }
        public object banner { get; set; }
        public object background { get; set; }
        public User[] users { get; set; }
    }

    public class User
    {
        public bool mature { get; set; }
        public string status { get; set; }
        public string broadcaster_language { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string _id { get; set; }
        public string logo { get; set; }
        public string video_banner { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public bool partner { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int followers { get; set; }
    }
}