using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnMessageReceivedController : ControllerBase
    {
        // GET: api/OnMessageReceived
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OnMessageReceived/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OnMessageReceived
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/OnMessageReceived/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
