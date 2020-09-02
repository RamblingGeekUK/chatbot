using FaunaDB.Client;
using FaunaDB.Types;
using System;
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

        [FaunaField("DateTime")]
        public DateTime DateTime { get; set; }

        [FaunaField("url")]
        public string URL { get; set; }
       
    }
}
