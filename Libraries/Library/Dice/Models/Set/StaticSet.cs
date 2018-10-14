using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectInspiration.Library.Dice.Models
{
    public class StaticSet : IResultSet
    {
        int value;

        public int Value => value;

        public string Display => value.ToString();

        public List<IRoll> Rolls => new List<IRoll> { new StaticRoll(value) };

        [JsonConstructor]
        public StaticSet(int value)
        {
            this.value = value;
        }
    }
}
