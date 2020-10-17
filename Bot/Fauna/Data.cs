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

        public static async Task GetListVectorPronuciationAsync(FaunaClient client)
        {

            Value value = await client.Query(Get(Ref(Collection("vectorpronunciation"), "279657734277693957")));
            //VIP vip = Decoder.Decode<VIP>(value.At("data"));

            IResult<VIP> vip = value.At("data").To<VIP>();
            vip.Match(
                Success: p => Console.WriteLine(">: {0}", p.TwitchDisplayName),
                Failure: reason => Console.WriteLine($"Something went wrong: {reason}")
            );


            //VIP viploaded = value.At("data").To<VIP>().Value;

           // Console.WriteLine("Twitch : {0}", viploaded.VectorProronunciation);

            // List<VIP> vip = new List<VIP>();
            
            

            //IResult<VIP> product = value.At("data").To<VIP>();
            //product.Match(
            //    Success: p => Console.WriteLine("Twitch Name: {0}, Vector Name {1}", p.TwitchDisplayName, p.VectorProronunciation),
            //    Failure: reason => Console.WriteLine($"Something went wrong: {reason}")
            //);

            // or even:

            //Product productLoaded = value.At("data").To<Product>().Value;
            //Console.WriteLine("Product loaded: {0}", prod.Description);
            //var names = JsonConvert.DeserializeObject<VIP>(result);
            //new List<string>(team.users.Select(c => c.display_name));
            //return new List<string>(names.VectorProronunciation.Select(c => c.));

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
