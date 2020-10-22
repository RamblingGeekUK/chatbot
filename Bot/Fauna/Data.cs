using FaunaDB.Client;
using FaunaDB.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public static async Task WriteVectorPronuciation(FaunaClient client, string TwitchDisplayName, string VectorProronunciation)
        {
            VIP vip = new VIP(TwitchDisplayName, VectorProronunciation);

            await client.Query(
                Create(
                    Collection("vectorpronunciation"),
                    Obj("data", Encoder.Encode(vip))
                    )
                );
        }

        static void ProcessData(Value[] values)
        {

            //List<VIP> users = values;

            //foreach (Value value in values)
            //{
            //    Console.WriteLine("{0}", value.toA);
            //}
        }
           
        public static async Task GetListVectorPronuciationAsync(FaunaClient client)
        {

            Value result = await client.Query(Paginate(Match(Index("vpronunciation")), size: 10));

            IResult<Value[]> data = result.At("data").To<Value[]>();

            data.Match(
                Success: value => ProcessData(value),
                Failure: reason => Console.WriteLine($"Something went wrong: {reason}")
            );




        }
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
