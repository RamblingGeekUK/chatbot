using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FaunaDB.Query.Language;

namespace ChatBot.Fauna
{
    public class Data
    {  
        public static async Task WriteLink(FaunaClient client, string capturedlink)
        {
            Links link = new Links(DateTime.UtcNow, capturedlink);

            await client.Query(
                Create(
                    Collection("links"),
                    Obj("data", Encoder.Encode(link))
                    )
                );
        }

        public static async Task GetVectorPronunciation(FaunaClient client, string twitchdisplayname)
        {
            Value result = await client.Query(
                        Get(
                        Match(Index("vectorprounciation_get_twitch_user"), twitchdisplayname)));

            TwitchUsers twitchusers = Decoder.Decode<TwitchUsers>(result.At("data"));
            Console.WriteLine("{0} : {1}", twitchusers.VectorProronunciation, twitchusers.TwitchDisplayName);

        }

    public static async Task GetVectorPronunciationAll(FaunaClient client)
        {

            Value value = await client.Query(Paginate(Match(Index("vpronunciation"))));


            TwitchUsers twitchUsers = value.At("data").To<TwitchUsers>().Value;
            Console.WriteLine(" loaded: {0}", twitchUsers.VectorProronunciation);

            //Value result = await client.Query(
            //            Get(
            //            Match(Index("vpronunciation"))));

            //IResult<Value> data = result.At("data").To<Value>();

            //data.Match(
            //    Success: value => ProcessData(value),
            //    Failure: reason => Console.WriteLine($"Something went wrong: {reason}")
            //);

            //TwitchUsers twitchusers = (TwitchUsers)result.At("data").To<TwitchUsers>();
            //Console.WriteLine(twitchusers.TwitchDisplayName);
        }

 
        static void ProcessData(Value values)
        {
            List<TwitchUsers> twitchUsers = new List<TwitchUsers>();
            
            Console.WriteLine(values);
           
        }

    }
    class TwitchUsers
    {
        [FaunaField("TwitchDisplayName")]
        public string TwitchDisplayName { get; set; }
        [FaunaField("VectorProronunciation")]
        public string VectorProronunciation { get; set; }
    }

    class Links
    {
        private DateTime _utcNow;
        private string _v;

        public Links(DateTime utcNow, string v)
        {
            _utcNow = utcNow;
            _v = v;
        }

        [FaunaField("url")]
        public string URL { get; set; }
       
    }
}
