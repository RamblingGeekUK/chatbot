# ChatBot

Well thats what it does. 😃  This is the bot for my Twitch Channel...

This is a ChatBot using TwitchLib and a Vector C# Library [Vector.NET](https://github.com/zaront/vector) to control Vector, he's a small robot.  Orginal launched by Anki now owned by Digital [Dream Labs](https://www.digitaldreamlabs.com/)

The ChatBot is using [TwitchLib](https://github.com/TwitchLib/TwitchLib) to connect to the TwitchChat and allows people in the chat to make Vector speak and tell jokes, etc.

It also captures links from chat and writes them to a FaunaDB database and also posts them into a StreamLinks channel in my discord server for later reference. 

There are a few projects in this solution

* Bot - Handles commands from chat and Contains the Vector code

* BotAPI - Planned API to the allow controlling of vector outside of the Chat - Not yet implenmented.

* BotOverlays - Working on the bot running the overlays for the stream  this listens for the the chat
  from signalr which is being sent from Bot - Work in progres

* LibGRPc is the library require for Vector.NET to run on a RaspberryPI. 

* ClientConsole - Test Signalr console application

