using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    class StaticValue : IRoll
    {
        int value;

        public int Value => value;

        public int Sides => int.MaxValue;

        public StaticValue(int value)
        {
            this.value = value;
        }
    }
}
