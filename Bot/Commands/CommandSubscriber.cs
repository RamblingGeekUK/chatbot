using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;


namespace ChatBot.Base
{
    public class CommandSubscriber : CommandBase, ICommand
    {
        public CommandSubscriber(TwitchClient client)
            : base(client)
        {
        }

        public void Execute(OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }

        public void Execute(OnChatCommandReceivedArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
