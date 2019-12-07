using System;
using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace ChatBot.Base
{
    public class CommandTellJoke : CommandBase, ICommand
    {
        public CommandTellJoke(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            client.SendMessage(e.Command.ChatMessage.Channel, "Sending..");

            var joke = SelectJoke();

            this.MessageChat(e.Command.ChatMessage.Channel, joke);
            new CommandAnnounce(client).Execute(joke, e);
            Helpers.StatusInfo($"{joke}", "vector");
        }

        private string SelectJoke()
        {
            var random = new Random();

            List<string> Joke = new List<string>
            {
                "Don't bash tech. Unless you're in the command line.",
                "Knock Knock, Async Function, Who's there?",
                "How do you get the code for the bank vault?, You checkout their branch.",
                "How did the developer announce their engagement?, They returned true",
                "What do you call a busy waiter?, A server.",
                "What do you call an idle server? A waiter.",
                "Please enter password, fornite, Error: Password is two weak",
                "How many Prolog programmers does it take to change a lightbulb? Yes",
                "I’ve been hearing news about this big boolean. Huge if true.",
                "What diet did the ghost developer go on? Boolean",
                "Why was the developer unhappy at their job? They wanted arrays.",
                "Why did 10 get paid less than '10'? There was workplace inequality.",
                "Why was the function sad after a successful first call? It didn’t get a callback.",
                "Why did the angry function exceed the callstack size? It got into an Argument with itself",
                "Whats the object-oriented way to become wealthy? Inheritance",
                "Why did the developer ground their kid? They weren't telling the truthy",
                "What did the array say after it was extended? Stop objectifying me.",
                "not false It's funny 'cause it's true.",
                "Where did the parallel function wash its hands? Async",
                "I'm starting a band called HTML Encoder, Looking to buy a guitar amp",
                "Why did the functions stop calling each other? Because they had constant arguments.",
                "What's the second movie about a database engineer called? The sequel",
                "A programmer's significant other tells them, Run to the store and pick up a loaf of bread.If they have eggs, get a dozen. The programmer comes home with 12 loaves of bread.",
                "What did the spider do on the computer? Made a website!",
                "What did the computer do at lunchtime? Had a byte!",
                "What does a baby computer call his father? Data",
                "Why did the computer keep sneezing? It had a Virus!",
                "What is a computer virus? A terminal illness!",
                "Why was there a bug in the computer? Because it was looking for a byte to eat?",
                "Why did the computer squeak? Because someone stepped on its mouse!",
                "What do you get when you cross a computer and a life guard? A screensaver!",
                "Where do all the cool mice live? In their mousepads!",
                "What do you get when you cross a computer with an elephant?  Lots of memory!",
                "How do programming pirates pass method parameters? Varrrrarrrgs.",
                "How do programming shepherds count their flock? With lambda functions",
                "How did pirates collaborate before computers ? Pier to pier networking.",
                "Why don't bachelors like Git? Because they are afraid to commit.",
                "A SQL query goes into a bar, walks up to two tables and asks: Can I JOIN you?",
                "How does a developer make a cheer? hip hip array!",
                "Why was the developer's family upset with them at dinner? They forgot to git squash before going home",
                "What did JavaScript call his son? JSON!",
                "What did the proud React component say to its child? I've got to give you props",
                "What did the server say to his client who was having a bad day? Everything's going to be 200",
                "Why did the developer go broke? Because they used up all their cache",
                "Are computers dangerous? Nah, they don't byte. They just nibble a bit.",
                "How did the mafioso kill the Node server? Tie await to it and let it async.",
                "You know what the best thing about booleans is?  Even if you are wrong, you are only off by a bit",
                "Why couldn’t the user update a file on a shared server? They didn’t have the write permissions",
                "What do you do when you can't understand your husband's behavior? man man",
                "How many developers does it take to change a light bulb? None, it's a hardware issue.",
                "Why do programmers always mix up Halloween and Christmas? Because 31 OCT == 25 DEC",
                "What are computers' favorite snacks? Microchips, phish sticks, and cookies. But just a few bytes of each.",
                "What do computers love to do at the beach? Put on some spam block for protection so they can safely surf the net while catching some .WAVs!",
                "What's a compiler developer's favorite spice? Parsley.",
                "A SQL developer walked into a NoSQL bar. They left because they couldn't find a table.",
                "How do you help JS errors? You console them!",
                "How do you help JS errors? You console them! Because they don't want them playing with matches",
                "Why didn't the div get invited to the dinner party? Because it had no class!",
                "Why did the constant break up with the variable? Because they changed.",
                "Why did the database administrator leave his wife? She had one-to-many relationships.",
                "Asynchronous JavaScript is amazing. I Promise you, await and see.",
                "What did the Class say in court when put on trial? I strongly object!",
                "Why do Java developers wear glasses? Because the don't c#",
                "What are the three hardest problems in computer science?  Naming things and off-by-one errors",
                "What did the fruit basket say to the developer? I hope you're ready for som pear programming!",
                "I've got a great UDP joke but I'm afraid you wouldn't get it.",
                "A programmer was arrested for writing unreadable code. They refused to comment",
                "There are 10 types of people in this world, those who understand binary and those who don't.",
                "I love you and I only love you. Does that turn you on? AND GATE: No.",
                "Why do all HTML emails get blocked? Because they are all span",
                "What did the process say after working in an infinite loop all day? I need a break.",
                "An Agent died unexpectedly. How was the crime solved? By looking at the Stack Trace.",
                "Why did the document store go out of business? It had NoSQL",
                "Why can't SQL and NoSQL Developers date one other? Because they don't agree on relationships.",
                "Why is Python like the Soviet Union? Because it has no private fields.",
                "Where did the API go to eat? to the RESTaurant",
                "Why shouldn't you trust Matlab developers? Because they're always plotting something.",
                "Why did the developer have to quit smoking?  Because they couldn't afford to pay the new syntax.",
                "How does a programmer open a jar for their significant other? They install Java",
                "What did the psychic say to the developers? I see dev people",
                "Where does the pirate stash all of their digital treasures? RAR",
                "What is React's favorite movie genre? Suspense",
                "Why couldn't the React component understand the joke? Because it didn't get the context",
                "What did XHR say to AJAX when it thought it was being a Mean Girl? Stop trying to make fetch happen!",
                "What was Grace Hopper's favorite car? VW Bug",
                "What sits on a pirate's shoulder and calls, Pieces of seven, Pieces of seven? Parity error.",
                "What is a pirate's favorite programming language? You'd think it was R, but a pirate's first love is Objectively C.",
                "Why did the programmer come home crying? His friends were always boolean him.",
                "Knock Knock, An async function, Who's there?",
                "What PostgreSQL library should Python developers use for adult-oriented code? psycoPG13",
                "What accommodations did the JavaScript developer request at the hotel? a room wit a Vue",
                "Where do developers drink? The Foo Bar",
                "Why do assembly programmers need to know how to swim? Because they work below C level.",
                "Who used the internet before it was cool? A Dell.",
                "Why did the web developer always go to the wrong hotel room? They were in room 301",
                "How do you stop a web developer stealing your stuff? Write 403 on it.",
                "Why are machine learning models so fit? Becauser they do weight training.",
                "Why did Gargamel shut down the internet? Because he didn't want people SMURFING the web!",
                "What did the command line die of? A Terminal illness.",
                "What did the command line die of ? A Terminal illness. Made a hash of it."
            };


            int index = random.Next(Joke.Count);
            return Joke[index];
        }
    }
}
