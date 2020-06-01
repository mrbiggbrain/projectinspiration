using System;
using System.Collections.Generic;
using System.Linq;
using ProjectInspirationLibrary.Dice.Parser;

namespace ProjectInspirationLibrary.Dice
{
    public class RollRequest
    {
        private int count;
        private int sides;
        private FilterType filterType;
        private int filterValue;
        private int Negative = 1;

        public RollRequest(int count, int sides, Parser.FilterType filterType, int filterValue)
        {
            this.count = count;
            this.sides = sides;
            this.filterType = filterType;
            this.filterValue = filterValue;
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

            if(this.count == 0)
            {
                RollResult r = new RollResult(sides * Negative, sides);
                r.valid = true;
                results.Add(r);
            }

            foreach(int i in Enumerable.Range(1, count))
            {
                int roll = (random.Next(sides) + 1) * Negative;

                results.Add(new RollResult(roll, sides));
            }

            if(filterType == FilterType.KEEP_HIGH)
            {
                var validItems = (from result in results orderby result.result descending select result).Take(filterValue);

                foreach(var item in validItems)
                {
                    item.valid = true;
                }
            }
            else if (filterType == FilterType.KEEP_LOW)
            {
                var validItems = (from result in results orderby result.result ascending select result).Take(filterValue);

                foreach (var item in validItems)
                {
                    item.valid = true;
                }
            }

            return results;
        }
    }
}