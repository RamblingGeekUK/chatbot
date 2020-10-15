using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TwitchLib.Api.Helix.Models.Common;
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

        protected void MessageChat(string channel, string message)
        {

            //ChatBot.
            //Console.WriteLine($"CommandBase MessageChat : {message}");
            
            this.client.SendMessage(channel, message);
        }       
        
         public void PlaySFX(string sfxname)
                {
                    bool _isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                    bool _isLinux = true; //System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

                    string[] Sourcetags = new string[]{"Raid", "Raiding"};
                    
                    List<SFX> soundfiles = new List<SFX>();
                    soundfiles.Add(new SFX() { filename="Raid Party.mp3", path="/home/pi/chatbot/sfx/", format= "mp3", description="A group of people shout RAIDING party!!", tag = Sourcetags });
                    soundfiles.Add(new SFX() { filename = "fdfsdf.mp3", path = "/home/pi/chatbot/sfx/", format = "mp3", description = "A group of people shout RAIDING party!!" });
                    soundfiles.Add(new SFX() { filename = "dsfsdfsdf.mp3", path = "/home/pi/chatbot/sfx/", format = "mp3", description = "A group of people shout RAIDING party!!" });
                    soundfiles.Add(new SFX() { filename = "sdfsdfsdf.mp3", path = "/home/pi/chatbot/sfx/", format = "mp3", description = "A group of people shout RAIDING party!!" });

                    //string filename = "Raiding_Party.mp3";
                    if (_isLinux)
                    {
                        //var output = $"omxplayer /home/pi/chatbot/sfx/{filename}".Bash();
                        Console.WriteLine(">>>>> {0}", 
                            soundfiles.Find(x => x.filename.ToLower().Contains(sfxname.ToLower())).tag[1]);
                    }
                }
    }

    public static class ShellHelper
    {
       
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}