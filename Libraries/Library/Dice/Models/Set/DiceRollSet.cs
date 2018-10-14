using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ProjectInspiration.Library.Dice;
using Newtonsoft.Json;
using ProjectInspiration.Library.JSON;

namespace ProjectInspiration.Library.Dice.Models
{
    public class DiceRollSet : IResultSet
    {
        public int Value
        {
            get
            {
                return Rolls.Sum(r => r.Value);
            }
        }

        //[JsonConverter(typeof(ConcreteTypeConverter<List<DiceRoll>>))]
        [JsonProperty("rolls")]
        public List<IRoll> Rolls { get; set; }

        [JsonConstructor]
        public DiceRollSet(IEnumerable<IRoll> rolls)
        {
            //Console.WriteLine("BADMAN");

            this.Rolls = rolls.ToList();
        }

    }
}
