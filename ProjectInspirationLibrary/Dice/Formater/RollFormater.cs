using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectInspirationLibrary.Dice.Formater
{
    public static class RollFormater
    {
        public static String Format(List<List<RollResult>> result)
        {
            String display = "";

            int i = 0;

            foreach (var set in result)
            {

                if (i > 0) display += ", ";

                if (set.Count > 1) display += "(";

                int y = 0;

                foreach (var roll in set)
                {
                    if (y > 0) display += ", ";

                    if (roll.IsCrit()) display += "**";
                    if (!roll.valid) display += "~~";

                    display += $"{roll.result}";

                    if (!roll.valid) display += "~~";
                    if (roll.IsCrit()) display += "**";

                    y++;
                }
                if (set.Count > 1) display += ")";

                i++;
            }

            var totalSum = result.Sum(x => x.Where(y => y.valid).Sum(z => z.result));
            // var totalSum = result.Sum(x => x.Sum(y => y.result));

            display += $" = {totalSum}";

            return display;
        }
    }
}
