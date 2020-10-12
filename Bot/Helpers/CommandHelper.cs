using ChatBot.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client;

namespace ChatBot.Helpers
{
    public static class CommandHelper
    {
        public static Dictionary<string, ICommand> GetCommands(TwitchClient client) {

            return new Dictionary<string, ICommand>
                {
                    { "alive", new CommandALive(client) },
                    { "vector-say", new CommandSay(client) },
                    { "vs", new CommandSay(client) },
                    { "sfx", new CommandSFX(client) },
                    { "vector-joke", new CommandTellJoke(client) },
                    { "attention", new CommandAttention(client) },
                    { "lurk", new CommandLurk(client) },
                    { "unlurk", new CommandUnLurk(client) },
                    { "commands", new CommandCommands(client) },
                    { "scene", new CommandScene(client) },
                    { "vector-bat", new CommandBat(client) },
                    { "vector-vol", new CommandVol(client) },
                    { "vector-move", new CommandMove(client) },
                    { "vector-time", new CommandTime(client) },
                    { "vector-play", new CommandPlay(client) },
                    { "vector-whisper", new CommandWhisper(client) }
                };
        }
    }
}
