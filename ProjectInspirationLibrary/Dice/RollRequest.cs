using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectInspirationLibrary.Dice
{
    public class RollRequest
    {
        private int count;
        private int sides;
        private int Negative = 1;

        public RollRequest(int count, int sides)
        {
            this.count = count;
            this.sides = sides;
        }

        public RollRequest IsNeg()
        {
            this.Negative = -1;
            return this;
        }

        internal List<RollResult> Roll()
        {

            List<RollResult> results = new List<RollResult>();
            Random random = new Random();

            foreach(int i in Enumerable.Range(1, count))
            {
                int roll = (random.Next(sides) + 1) * Negative;

                results.Add(new RollResult(roll, sides));
            }

            return results;
        }
    }
}