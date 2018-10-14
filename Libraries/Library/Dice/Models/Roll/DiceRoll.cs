using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectInspiration.Library.Dice.Models
{
    public class DiceRoll : IRoll
    {
        [JsonProperty("value")]
        public int Value { get; }

        [JsonProperty("sides")]
        public int Sides { get; }

        [JsonConstructor]
        public DiceRoll(int sides, int value)
        {
            this.Sides = sides;
            this.Value = value;
        }
    }
}
