using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    public class DiceRoll : IRoll
    {
        public int Value { get; }
        public int Sides { get; }

        public DiceRoll(int sides, int value)
        {
            this.Sides = sides;
            this.Value = value;
        }
    }
}
