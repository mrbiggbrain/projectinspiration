using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectInspiration.Library.Dice.Models
{
    class StaticRoll : IRoll
    {
        int value;

        public int Value => value;

        public int Sides => int.MaxValue;

        [JsonConstructor]
        public StaticRoll(int value)
        {
            this.value = value;
        }
    }
}
