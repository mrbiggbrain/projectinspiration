using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {
        // GET: api/Dice
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dice/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return id;
        }

        // POST: api/Dice
        [HttpPost]
        public JsonResult Post([FromBody] RollRequest request)
        {
            return new JsonResult(RollProcessor.Roll(request));
        }

        // PUT: api/Dice/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] object value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
