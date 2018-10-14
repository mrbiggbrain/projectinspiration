using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ProjectInspiration.Library.JSON;

namespace ProjectInspiration.Library.Dice.Models.Responce
{
    public class RollResult : IRollResult
    {
        //[JsonConverter(typeof(ConcreteTypeConverter<List<DiceRollSet>>))]
        [JsonProperty("sets")]
        public List<IResultSet> Sets { get; set; }


        [JsonConstructor]
        public RollResult(List<IResultSet> sets)
        {
            this.Sets = sets;
        }

        public int Value
        {
            get
            {
                return Sets.Sum(s => s.Value);
            }
        }

        public RollResult(IEnumerable<IResultSet> sets)
        {
            this.Sets = sets.ToList();
        }
    }
}
