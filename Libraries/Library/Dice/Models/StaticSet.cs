using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    public class StaticSet : IResultSet
    {
        int value;

        public int Value => value;

        public string Display => value.ToString();

        public List<IRoll> Rolls => new List<IRoll> { new StaticValue(value) };

        public StaticSet(int value)
        {
            this.value = value;
        }
    }
}
