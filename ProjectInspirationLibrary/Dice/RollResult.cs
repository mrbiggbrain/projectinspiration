using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspirationLibrary.Dice
{
    public class RollResult
    {
        public int result;
        public int max;

        public RollResult(int result, int max)
        {
            this.result = result;
            this.max = max;
        }

        public bool IsCrit()
        {
            return result == max;
        }

        public bool IsCritFail()
        {
            return result == 1;
        }
    }
}
