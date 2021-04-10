using FaunaDB.Types;
using System;

namespace ChatBot
{
    public class VIP
    {
        [FaunaConstructor]
        public VIP(string twitchDisplayName, string vectorProronunciation)
        {
            this.TwitchDisplayName = twitchDisplayName;
            this.VectorProronunciation = vectorProronunciation;
        }
        
        [FaunaField("TwitchDisplayName")]
        public string TwitchDisplayName { get; set; }

        [FaunaField("VectorProronunciation")]
        public string VectorProronunciation { get; set; }
    }
}